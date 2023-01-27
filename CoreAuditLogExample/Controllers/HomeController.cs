using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAuditLogExample.Models;
using CoreAuditLogExample.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAuditLogExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /<controller>/  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductAdd()
        {
            var product = new Product
            {
                Name = "Apple",
                Description = "It is an apple",
                CreatedBy = 1
                
            };
            _productService.Add(product);
            return View();
        }

        public IActionResult ProductDelete(int id)
        {
            var product = _productService.Get(id);
            _productService.Delete(product);
            return View();
        }

        public IActionResult ProductUpdate(Product product)
        {
            product.LastModified = DateTime.Now;
            _productService.Update(product);
            return View();
        }


    }
}

