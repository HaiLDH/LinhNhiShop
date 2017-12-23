using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinhNhiShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        [MaxLength(256)]
        public string CustomerName { get; set; }

        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [MaxLength(256)]
        public string CustomerMessage { get; set; }

        [MaxLength(256)]
        public string Paymentmethod { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string PaymentStatus { get; set; }

        public bool Status { get; set; }

        [MaxLength(128)]
        public string CustomerId { get; set; }
    }
}