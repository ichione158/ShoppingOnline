using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingOnline.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Danh mục")]
        public string name { get; set; }
        [Required]
        [DisplayName("Trạng thái")]
        public bool valid { get; set; }

        ICollection<Product> products { get; set; }
    }
}