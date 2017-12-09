using AutoMapper;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using LinhNhiShop.Web.Infrastructue.Core;
using LinhNhiShop.Web.Infrastructue.Extentions;
using LinhNhiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace LinhNhiShop.Web.Api
{
    [RoutePrefix(("api/product"))]
    //[Authorize]
    public class ProductController : ApiBaseController
    {
        IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage httpRequestMessage, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = responData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };


                var respon = httpRequestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return respon;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage httpRequestMessage, int id)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                var model = _productService.GetById(id);

                var responData = Mapper.Map<Product, ProductViewModel>(model);

                var respon = httpRequestMessage.CreateResponse(HttpStatusCode.OK, responData);
                return respon;
            });
        }


        [Route("getallparent")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage httpRequestMessage)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                var model = _productService.GetAll();

                var responData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                var respon = httpRequestMessage.CreateResponse(HttpStatusCode.OK, responData);
                return respon;
            });
        }


        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage httpRequest, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(httpRequest, () =>
            {
                HttpResponseMessage httpResponse = null;

                if (!ModelState.IsValid)
                {
                    httpResponse = httpRequest.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var product = new Product();
                    product.UpdateProduct(productViewModel);
                    product.CreateDate = DateTime.Now;
                    product.CreateBy = User.Identity.Name;

                    _productService.Add(product);
                    _productService.Save();

                    var responData = Mapper.Map<Product, ProductViewModel>(product);
                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.Created, responData);
                }

                return httpResponse;
            });
        }


        [HttpPut]
        [Route("update")]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage httpRequest, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(httpRequest, () =>
            {
                HttpResponseMessage httpResponse = null;

                if (!ModelState.IsValid)
                {
                    httpResponse = httpRequest.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var product = _productService.GetById(productViewModel.ID);
                    product.UpdateProduct(productViewModel);
                    product.UpdateDate = DateTime.Now;
                    product.CreateBy = User.Identity.Name;

                    _productService.Update(product);
                    _productService.Save();

                    var responData = Mapper.Map<Product, ProductViewModel>(product);
                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.Created, responData);
                }

                return httpResponse;
            });
        }


        [HttpDelete]
        [Route("delete")]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage httpRequest, int id)
        {
            return CreateHttpResponse(httpRequest, () =>
            {
                HttpResponseMessage httpResponse = null;

                if (!ModelState.IsValid)
                {
                    httpResponse = httpRequest.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProduct = _productService.Delete(id);
                    _productService.Save();

                    var responData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK, responData);
                }

                return httpResponse;
            });
        }

        [HttpDelete]
        [Route("deletemulti")]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage httpRequest, string checkedProducts)
        {
            return CreateHttpResponse(httpRequest, () =>
            {
                HttpResponseMessage httpResponse = null;

                if (!ModelState.IsValid)
                {
                    httpResponse = httpRequest.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProductId = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var id in listProductId)
                    {
                        _productService.Delete(id);
                    }
                    _productService.Save();

                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.OK, listProductId.Count);
                }

                return httpResponse;
            });
        }
    }
}
