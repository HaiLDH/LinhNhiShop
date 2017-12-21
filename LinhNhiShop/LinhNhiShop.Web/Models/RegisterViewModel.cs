using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu tối thiểu 6 kí tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Yêu cầu Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}