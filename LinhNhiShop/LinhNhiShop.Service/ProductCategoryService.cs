﻿using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Data.Repositories;
using LinhNhiShop.Model.Models;
using System.Collections.Generic;

namespace LinhNhiShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);
        void Update(ProductCategory ProductCategory);
        ProductCategory Delete(ProductCategory ProductCategory);
        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAllByParentId(int parentID);

        IEnumerable<ProductCategory> GetAll(string keyword);

        ProductCategory GetById(int id);

        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _ProductCategoryRepository;
        IUnitOfWork _unitOfWork;
        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._unitOfWork = unitOfWork;
        }


        public ProductCategory Add(ProductCategory ProductCategory)
        {
            return _ProductCategoryRepository.Add(ProductCategory);
        }

        public ProductCategory Delete(ProductCategory ProductCategory)
        {
            return _ProductCategoryRepository.Delete(ProductCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _ProductCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.Alias.Contains(keyword));
            else
                return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentID)
        {
            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentID);
        }

        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _ProductCategoryRepository.Update(ProductCategory);
        }
    }
}
