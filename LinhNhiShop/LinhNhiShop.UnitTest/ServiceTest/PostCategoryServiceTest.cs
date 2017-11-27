using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Data.Repositories;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinhNhiShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRespository> _mockPostCategoryRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listPostCategory;

        [TestInitialize]
        public void Initialeze()
        {
            _mockPostCategoryRepository = new Mock<IPostCategoryRespository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockPostCategoryRepository.Object, _mockUnitOfWork.Object);
            _listPostCategory = new List<PostCategory>()
            {
                new PostCategory(){ ID=1,Name="Category1",Status=true},
                new PostCategory(){ ID=1,Name="Category1",Status=true},
                new PostCategory(){ ID=1,Name="Category1",Status=true}
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockPostCategoryRepository.Setup(m => m.GetAll(null)).Returns(_listPostCategory);

            //call action
            var result = _postCategoryService.GetAll() as List<PostCategory>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test Name";
            postCategory.Alias = "test-name";
            postCategory.Status = true;

            _mockPostCategoryRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
              {
                  p.ID = 1;
                  return p;
              });

            var result = _postCategoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
