using Newtonsoft.Json;
using ProductsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProductsMVC.Services
{
    public class RESTService
    {
        readonly string uri = "http://localhost:53284/api/Products/";

        public async Task<List<Product>> GetProductsAsync(CancellationToken ct)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                #region 
                /*
                if (ct.IsCancellationRequested)
                {
                    //return new List<Product>() { new Product() { Name = "Cancelled", Price = 6, ArticleNumber = 7 }};
                }*/
                #endregion
                //Add more settings for httpClient 
                return JsonConvert.DeserializeObject<List<Product>>(
                    await httpClient.GetStringAsync(uri)
                );
            }
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {               
                return JsonConvert.DeserializeObject<Product>(
                    await httpClient.GetStringAsync(uri + id.ToString()));
            }
        }
    }
}