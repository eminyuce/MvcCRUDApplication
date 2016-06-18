using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCRUDApplication.Entities;
using MvcCRUDApplication.Repositories;
using PagedList;

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
        public ActionResult ProductPaging(int ? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                page = 1;
            }

            // retrieve list from database/whereverand
            var listUnpaged = ProductRepository.GetProducts();

            // page the list
            const int pageSize = 20;
            var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return View(listPaged);
        }
    }
}
