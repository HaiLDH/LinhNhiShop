using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Data.Repositories;
using LinhNhiShop.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNhiShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory _dbFactory;
        IUnitOfWork _unitOfWork;
        IPostCategoryRepository _objRepository;

        [TestInitialize]
        public void Initialize()
        {
            _dbFactory = new DbFactory();
            _objRepository = new PostCategoryRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test Category Name";
            category.Alias = "test-category-name";
            category.Status = true;

            var result = _objRepository.Add(category);
            _unitOfWork.Commit();


            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ID);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = _objRepository.GetAll();
            Assert.AreEqual(3, list.Count());
        }
    }
}
