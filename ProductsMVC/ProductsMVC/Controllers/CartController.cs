using Klarna.Api;
using Klarna.Checkout;
using Newtonsoft.Json.Linq;
using ProductsMVC.Models;
using ProductsMVC.WebUI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProductsMVC.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        #region
        //Clarna Test shared secret
        string Shared_Secret = "tE94QeKzSdUVJGe";
        #endregion

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        //Cart object will be created and handled by ProductsMVC.Infastructure.CartModelBinder
        //Setup in global.asax

        public ViewResult Index(Cart cart, string returnUrl) {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public ViewResult Checkout(Cart cart)
        {
            var cartItems = new List<Dictionary<string, object>>();
           
                foreach(CartLine item in cart.Lines)
                {
                cartItems.Add(new Dictionary<string, object>
                    {
                        { "reference", item.Product.ArticleNumber.ToString() },
                        { "name", item.Product.Name },
                        { "quantity", item.Quantity },
                        { "unit_price", (int)item.Product.Price * 100 },
                        { "tax_rate", 2500 }
                    });
                }
            cartItems.Add(
            new Dictionary<string, object>
                {
                    { "type", "shipping_fee" },
                    { "reference", "SHIPPING" },
                    { "name", "Shipping Fee" },
                    { "quantity", 1 },
                    { "unit_price", 4900 },
                    { "tax_rate", 2500 }
                });
        
            var cart_info = new Dictionary<string, object> { { "items", cartItems } };

            var data = new Dictionary<string, object>
            {
                { "cart", cart_info }
            };

            var merchant = new Dictionary<string, object>
            {
               { "id", "5160" },
               { "back_to_store_uri", "http://127.0.0.1:53284/" },
               { "terms_uri", "http://127.0.0.1:53284/Cart/Terms" },
            {
                "checkout_uri",
                "http://127.0.0.1:53284/sv/Cart/Checkout"
            },
            {
                "confirmation_uri",
                "http://127.0.0.1:53284/sv/Cart/OrderConfirmed" +
                "?klarna_order_id={checkout.order.id}"
            },
            {
                "push_uri",
                "https://snowroller.azurewebsites.net/Home/Callback" +
                "?secret="+Shared_Secret+"&klarna_order_id={checkout.order.id}"
            }
         };
            data.Add("purchase_country", "SE");
            data.Add("purchase_currency", "SEK");
            data.Add("locale", "sv-se");
            data.Add("merchant", merchant);
            
            var connector = Connector.Create(Shared_Secret, Connector.TestBaseUri);
            Order order = new Order(connector);
            order.Create(data);
            
            order.Fetch();

            // Store order id of checkout in session object.
            Session.Add("klarna_order_id", order.GetValue("id"));

            // Display checkout
            var gui = order.GetValue("gui") as JObject;
            var snippet = gui["snippet"];
                 
            return View(snippet);
        }

        public ViewResult OrderConfirmed(string klarna_order_id)
        {

            var connector = Connector.Create(Shared_Secret, Connector.TestBaseUri);

            var order = new Order(connector, klarna_order_id);

            order.Fetch();
          
            var gui = order.GetValue("gui") as JObject;
            var snippet = gui["snippet"];
            
            // Clear session object.
             Session["klarna_order_id"] = null;

            return View(snippet);
        }

        public string ActivateOrder(string rno)
        {

            Configuration configuration = new Configuration(
                Country.Code.SE, Language.Code.SV, Currency.Code.SEK,
                Encoding.Sweden)
            {
                Eid = 5160,
                Secret = "tE94QeKzSdUVJGe",
                IsLiveMode = false,
            };
          
            Klarna.Api.Api api = new Klarna.Api.Api(configuration);

            ActivateReservationResponse response = api.Activate(
                rno,
                string.Empty,                // OCR Number
                ReservationFlags.SendByEmail // Flags
            );

            return "Invoice number: " + response.InvoiceNumber;
        }


        public void OrderStatus()
        {
            
        }

        public RedirectToRouteResult AddToCart(Cart cart, int ID, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ID == ID);
            if (product != null) {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int ID, string returnUrl) {
            Product product = repository.Products.FirstOrDefault(p => p.ID == ID);
            if (product != null) {
               cart.RemoveLine(product);
            } return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }
        /* private Cart GetCart() {
             Cart cart = (Cart)Session["Cart"];
             if (cart == null) {
                 cart = new Cart();
                 Session["Cart"] = cart;
             }
             return cart;
         }*/
    }
}