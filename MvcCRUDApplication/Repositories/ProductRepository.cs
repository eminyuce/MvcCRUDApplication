using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using MvcCRUDApplication.Entities;
using MvcCRUDApplication.Helpers;

namespace MvcCRUDApplication.Repositories
{
    public class ProductRepository
    {

        public static List<Product> GetProductsFromCache()
        {
            string cacheKey = "ProductCache";
            var items = (List<Product>)MemoryCache.Default.Get(cacheKey);
            if (items == null)
            {
                items = GetProducts();
                CacheItemPolicy policy = null;
                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.Default;
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Settings.CacheMediumSeconds);
                MemoryCache.Default.Set(cacheKey, items, policy);
            }
            return items;
        }

        public static List<Product> GetProducts()
        {
            return DBDirectory.GetProducts();
        }
        public static int SaveOrUpdateProduct(Product item)
        {
            return DBDirectory.SaveOrUpdateProduct(item);
        }
        public static Product GetProduct(int productId)
        {
            return DBDirectory.GetProduct(productId);
        }
        public static void DeleteProduct(int productId)
        {
            DBDirectory.DeleteProduct(productId);
        }
    }

}