using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProductsMVC.Models;
using ProductsMVC.Filters;
using ProductsMVC.WebUI.ViewModels;
using ProductsMVC.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Security.Principal;

using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces;

namespace ProductsMVC.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository repository;
        
        //Number of objects on each page.
        public int PageSize = 3;

        public ProductsController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        private class Info
        {
            public string message;
            public int i;
        }
        public string session()
        {
            Info message;

            if (Session["Message"] == null)
            {
                message = new Info();
                message.message = "Initilized";
                message.i = 1;
                Session["Message"] = message;
                return "New session created and initilized";
            }
            else
            {
                message = (Info)Session["Message"];
                message.i++;
                Session["Message"] = message;
                return message.message + message.i;
            }
        }
        public ActionResult Callback(string oauth_token, string oauth_verifier)
        {
            //Verify that oauth_token == token recieved in Twitter Action.. Use a session variable?
            //if (!((OAuth_Token)Session["RequestToken"]).oauth_token.Equals(oauth_token))
     
            OAuth_Token token = new OAuth_Token { oauth_token = oauth_token, oauth_token_secret = oauth_verifier };

            //Step 3: Convert the request token to an access token
            TwitterService t = new TwitterService();
            OAuth_Token access_token  = t.OAuth_Convert_Request_To_Access_Token(token);

            //Store the access_token in users session for further use.
            Session.Add("Access_token", access_token);

            return RedirectToAction("Index");
        }
        public ActionResult Twitter(string search)
        {
            TwitterService t = new TwitterService();
            //Step 1: Obtaining a request token
            OAuth_Token token = t.OAuth_Request_Token();
            Session.Add("RequestToken", token);
            
            //Step 2: Redirecting the user
            return Redirect("https://api.twitter.com/oauth/authorize?oauth_token=" + token.oauth_token);
        }

        public string TwitterSettings()
        {
            //Step 3: Convert the request token to an access token
            TwitterService t = new TwitterService();

            return t.GetSettings((OAuth_Token)Session["Access_token"]);
        }

        public async Task<ActionResult> Remote()
        {
            //Using remote web api
            RESTService service = new RESTService();
            CancellationTokenSource cts = new CancellationTokenSource();

            Task<List<Product>> task = service.GetProductsAsync(cts.Token);

            //Cancel operation
            //cts.CancelAfter(1000);

            return View(await task);
            //return View(await service.GetProductByIdAsync(34872));
        }
        
        //Returns a View supporting dynamic loading of products from javascript
        //using api/Products
        public ViewResult SinglePage()
        {
            repository.Add(new Product() {Name="Product1"});

            return View();
        }

        // GET: Products
        public ViewResult Index(string searchString)
        {   
            var products = repository.Products.Include(p => p.Category);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            
            return View(products.ToList());
        }

        public ViewResult List(string category, int page = 1) {
            var products = repository.Products.Include(p => p.Category);
            
            ProductsListViewModel model = new ProductsListViewModel {
                Products = products.OrderBy(p => p.ID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfoViewModel { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = products.Count()
                }
            };
            return View(model);
        }
        
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,ArticleNumber,ImageURL,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Products.Add(product);
                repository.Context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,ArticleNumber,ImageURL,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Context.Entry(product).State = EntityState.Modified;
                repository.Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        //[Authorize]
        //[Authorize(Roles ="User")]
        [CustomAuth(false)]
        public ActionResult Delete(int? id)
        {
            //Check if user is authenticated and in the role admin
            //Request.IsAuthenticated
           /* if( !User.Identity.IsAuthenticated && !User.IsInRole("Admin"))
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Not an admin fool!");
            }*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repository.Products.Find(id);
            repository.Products.Remove(product);
            repository.Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult ListCategories()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
