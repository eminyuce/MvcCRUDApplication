using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCRUDApplication.Entities
{
    public class Product
    {
        public int ProductId { get; set; }


        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please product name")]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public float Price { get; set; }
        public Product()
        {

        }
        public Product(int productId, string name, string description, float price)
        {

            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.Price = price;

        }


        public override string ToString()
        {
            return String.Format(
            "ProductId:{0} Name:{1} Description:{2} Price:{3} ", ProductId, Name, Description, Price);
        }
    }

}