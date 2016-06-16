using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCRUDApplication.Entities;
using MvcCRUDApplication.Repositories;

namespace MvcCRUDApplication.Controllers
{
    public class ProductsController : Controller
    {
         

        public ActionResult Index()
        {
            var products = ProductRepository.GetProducts();
            return View(products);
        }
        public ActionResult ListDisplay()
        {
            var products = ProductRepository.GetProducts();
            return View(products);
        }
        public ActionResult SaveOrUpdateProduct(int id=0)
        {
            var product = ProductRepository.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult SaveOrUpdateProduct(Product product)
        {
            ProductRepository.SaveOrUpdateProduct(product);
            return RedirectToAction("Index");
        }
        public ActionResult ProductDetail(int id)
        {
            var product = ProductRepository.GetProduct(id);
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            ProductRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
