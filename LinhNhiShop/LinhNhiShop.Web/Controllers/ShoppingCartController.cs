using AutoMapper;
using LinhNhiShop.Common;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using LinhNhiShop.Web.App_Start;
using LinhNhiShop.Web.Infrastructue.Extentions;
using LinhNhiShop.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LinhNhiShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        IOrderService _orderService;
        ApplicationUserManager _applicationUserManager;

        public ShoppingCartController(IProductService productService,
            ApplicationUserManager applicationUserManager,
            IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
            _applicationUserManager = applicationUserManager;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return View();
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                var newShoppingCartViewModel = new ShoppingCartViewModel
                {
                    ProductId = productId
                };

                var product = _productService.GetById(productId);
                newShoppingCartViewModel.Product = Mapper.Map<Product, ProductViewModel>(product);
                newShoppingCartViewModel.Quantity = 1;

                cart.Add(newShoppingCartViewModel);
            }

            Session[CommonConstants.SessionCart] = cart;

            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetAll()
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCart(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            foreach (var cartSs in cartSession)
            {
                foreach (var cartVm in cartViewModel)
                {
                    if (cartSs.ProductId == cartVm.ProductId)
                        cartSs.Quantity = cartVm.Quantity;
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;

                return Json(new
                {
                    status = true
                });
            }

            return Json(new
            {
                status = false
            });
        }




        public ActionResult Checkout()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {

            }

            return View();
        }

        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _applicationUserManager.FindById(userId);

                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public JsonResult CreateOrder(string orderViewModel)
        {
            var orderVm = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);

            var orderNew = new Order();
            orderNew.UpdateOrder(orderVm);

            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreateBy = User.Identity.GetUserName();
            }

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart)
            {
                var detail = new OrderDetail
                {
                    ProductID = item.ProductId,
                    Quantity = item.Quantity
                };
                orderDetails.Add(detail);
            }

            _orderService.Create(orderNew, orderDetails);

            return Json(new
            {
                status = true
            });
        }

    }
}