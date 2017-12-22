﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Yêu cầu nhập tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}