using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webBangHangOnline.Models.EF
{
    [Table("tb_product")]
    public class Product :CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage =("Không được để trống mục này"))]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Alias { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string ProductCode { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = ("Không được để trống mục này"))]
        public string Detail { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        [Required(ErrorMessage = ("Không được để trống mục này"))]
        public decimal Price { get; set; }
        [Required(ErrorMessage = ("Không được để trống mục này"))]
        public decimal PriceSale { get;set; }
        [Required(ErrorMessage = ("Không được để trống mục này"))]
        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public int ProductCategoryId { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(500)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }
        public bool isActive { get; set; }
        public virtual ProductCategory productCategory { get; set; }
    }
}