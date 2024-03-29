﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models.EF
{
    [Table("tb_order")]
    public class Order : CommonAbstract
    {
        public Order() { 
            this.Details = new HashSet<OrderDetail>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được bỏ trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public int TypePayment { get; set; }

        public ICollection<OrderDetail> Details { get;set; }
    }
}