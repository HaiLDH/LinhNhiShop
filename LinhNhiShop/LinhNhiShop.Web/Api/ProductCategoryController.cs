using AutoMapper;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using LinhNhiShop.Web.Infrastructue.Core;
using LinhNhiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LinhNhiShop.Web.Infrastructue.Extentions;

namespace LinhNhiShop.Web.Api
{
    [RoutePrefix(("api/productcategory"))]
    public class ProductCategoryController : ApiBaseController
    {
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage httpRequestMessage, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
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
                var model = _productCategoryService.GetById(id);

                var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);

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
                var model = _productCategoryService.GetAll();

                var responData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

                var respon = httpRequestMessage.CreateResponse(HttpStatusCode.OK, responData);
                return respon;
            });
        }


        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage httpRequest, ProductCategoryViewModel productCategoryViewModel)
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
                    var productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.CreateDate = DateTime.Now;

                    _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();

                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.Created, responData);
                }

                return httpResponse;
            });
        }


        [HttpPut]
        [Route("update")]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage httpRequest, ProductCategoryViewModel productCategoryViewModel)
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
                    var productCategory = _productCategoryService.GetById(productCategoryViewModel.ID);
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.UpdateDate = DateTime.Now;

                    _productCategoryService.Update(productCategory);
                    _productCategoryService.Save();

                    var responData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    httpResponse = httpRequest.CreateResponse(HttpStatusCode.Created, responData);
                }

                return httpResponse;
            });
        }
    }
}
