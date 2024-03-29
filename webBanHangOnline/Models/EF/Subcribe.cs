﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models.EF
{
    [Table("tb_subcribe")]
    public class Subcribe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Không được để trống")]
        public string Email { get; set;}
        public DateTime CreatedDate { get; set; }
    }
}