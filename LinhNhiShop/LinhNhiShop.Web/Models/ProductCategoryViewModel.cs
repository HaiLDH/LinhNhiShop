using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Models
{
    public class ProductCategoryViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên danh mục sản phẩm lớn hơn 0 và ít hơn 256 kí tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tiêu đề SEO lớn hơn 0 và ít hơn 256 kí tự")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Mô tả tối đa 500 kí tự")]
        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }


        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái danh mục sản phẩm")]
        public bool Status { set; get; }
    }
}