using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required (ErrorMessage ="Category name is required")]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public bool Status { get; set; }

        public List<Product> Products { get; set; }
    }
}
