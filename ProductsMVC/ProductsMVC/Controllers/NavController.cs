using ProductsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductsMVC.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;
        public NavController(IProductRepository repo) {
            repository = repo;
        }
        public PartialViewResult Menu() {
            IEnumerable<string> categories = repository.Categories.Select(x => x.Name).OrderBy(x => x);
            return PartialView(categories);
        }
    }
}