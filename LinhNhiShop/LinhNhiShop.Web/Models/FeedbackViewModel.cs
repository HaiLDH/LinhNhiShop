using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [MaxLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "Nội dung không được quá 500 ký tự")]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetailViewModel { get; set; }
    }
}