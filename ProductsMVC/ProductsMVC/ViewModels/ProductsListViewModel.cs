using ProductsMVC.Models;
using System.Collections.Generic;

namespace ProductsMVC.WebUI.ViewModels
{
    public class ProductsListViewModel {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}