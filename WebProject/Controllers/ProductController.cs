using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.Repositories;

namespace WebProject.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        Context c = new Context();

        public IActionResult List()
        {
            return View(productRepository.TList("Category"));
        }

        [Authorize]
        public IActionResult Index()
        {

            return View(productRepository.TList("Category"));
        }
       
        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> getCatList = (from x in c.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString()
                                               }).ToList();
            ViewBag.CatList = getCatList;
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            productRepository.TAdd(p);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id)
        {

            productRepository.TDelete(new Product {ProductID=id });
            return RedirectToAction("Index");
        }

        public IActionResult GetProduct(int id)
        {
            var x = productRepository.TGet(id);
            List<SelectListItem> getCatList = (from y in c.Categories.ToList()
                                               select new SelectListItem    
                                               {
                                                   Text = y.CategoryName,
                                                   Value = y.CategoryID.ToString()
                                               }).ToList();
            ViewBag.CatList = getCatList;
            Product p = new Product()
            {
                ProductID=x.ProductID,
                Name = x.Name,
                Description=x.Description,
                Price=x.Price,
                ImageUrl=x.ImageUrl,
                Stock=x.Stock,
                Brand=x.Brand,
                Model=x.Model,
                CategoryID=x.CategoryID
            };
            return View(p);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
            var x = productRepository.TGet(p.ProductID);            
            x.Name = p.Name;
            x.Description = p.Description;
            x.Price = p.Price;
            x.ImageUrl = p.ImageUrl;
            x.Stock = p.Stock;
            x.Brand = p.Brand;
            x.Model = p.Model;
            x.CategoryID = p.CategoryID;
            productRepository.TUpdate(x);
            return RedirectToAction("Index");

        }

        /*private readonly ProductRepository productRepository;
        public ProductController(ProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public IActionResult Index()
        {
            
            return View(productRepository.TList("Category"));
        }
        public IActionResult List()
        {
            return View(productRepository.TList("Category"));
        }
        public IActionResult ProductDelete(int pd)
        {
            var dep = productRepository.c.Products.Find(pd);
            productRepository.TDelete(dep);
            return RedirectToAction("Index");
        }

        [HttpGet] //burada bunun işlevi ne yeni sayfa açıyoruz sonuçta ?
        public IActionResult NewProduct()
        {

            List<SelectListItem> degerler = (from x in productRepository.c.Categories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }
                                               ).ToList();
            ViewBag.dgr = degerler;

            return View("NewProduct");
        }

        [HttpPost]
        public IActionResult NewProduct(Product pd)
        {
            productRepository.TAdd(pd);
            return RedirectToAction("Index");
        }

        public IActionResult ProductGetir(int id)
        {
            var dep = productRepository.c.Products.Find(id);
            return View("ProductGetir", dep);
        }
        public IActionResult ProductUpdate(Product p)
        {
            var per = productRepository.c.Products.Find(p.ProductID);
            per.Name = p.Name;
            per.Description = p.Description;
            // dep.depId = d.depId;
            productRepository.c.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
