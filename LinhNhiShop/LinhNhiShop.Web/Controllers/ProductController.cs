﻿using AutoMapper;
using LinhNhiShop.Common;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using LinhNhiShop.Web.Infrastructue.Core;
using LinhNhiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinhNhiShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService,
            IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, out totalRow);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            int totalPage = (int)Math.Ceiling((double)(totalRow / pageSize));

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
    }
}