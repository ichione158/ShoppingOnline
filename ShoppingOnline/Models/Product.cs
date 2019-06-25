using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingOnline.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string name { get; set; }

        [Range(0,1000000)]
        [DisplayName("Giá thành")]
        public double price { get; set; }

        [Range(0, 1000)]
        [DisplayName("Số lượng")]
        public int amount { get; set; }

        [StringLength(500)]
        [DisplayName("Mô tả")]
        public string description { get; set; }
        
        [StringLength(250)]
        [DisplayName("Hình ảnh")]
        public string thumbnail { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public bool valid { get; set; }

        // Thiet lap khoa ngoai cateId den bang Categories
        [ForeignKey("category")]
        public int cateId { get; set; }
        public virtual Category category { get; set; }
    }
}