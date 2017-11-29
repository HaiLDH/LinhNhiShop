using LinhNhiShop.Model.Models;
using LinhNhiShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Infrastructue.Extentions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;

            postCategory.Name = postCategoryViewModel.Name;

            postCategory.Alias = postCategoryViewModel.Alias;

            postCategory.Description = postCategoryViewModel.Description;

            postCategory.ParentID = postCategoryViewModel.ParentID;

            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;

            postCategory.Image = postCategoryViewModel.Image;

            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;

            postCategory.CreateDate = postCategoryViewModel.CreateDate;

            postCategory.CreateBy = postCategoryViewModel.CreateBy;

            postCategory.UpdateDate = postCategoryViewModel.UpdateDate;

            postCategory.UpdateBy = postCategoryViewModel.UpdateBy;

            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;

            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;

            postCategory.Status = postCategoryViewModel.Status;
    }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;

            post.Name = postViewModel.Name;

            post.Alias = postViewModel.Alias;

            post.CategoryID = postViewModel.CategoryID;

            post.Image = postViewModel.Image;

            post.Description = postViewModel.Description;

            post.Content = postViewModel.Content;

            post.HomeFlag = postViewModel.HomeFlag;

            post.HotFlag = postViewModel.HotFlag;

            post.ViewCount = postViewModel.ViewCount;

            post.CreateDate = postViewModel.CreateDate;

            post.CreateBy = postViewModel.CreateBy;

            post.UpdateDate = postViewModel.UpdateDate;

            post.UpdateBy = postViewModel.UpdateBy;

            post.MetaKeyword = postViewModel.MetaKeyword;

            post.MetaDescription = postViewModel.MetaDescription;

            post.Status = postViewModel.Status;
        }
    }
}