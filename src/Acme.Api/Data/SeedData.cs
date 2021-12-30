using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Api.Data
{
    public static class SeedData
    {
        public static void Seed(AcmeDbContext context)
        {
            foreach(var name in new List<string>()
            {
                "Shoe", "Vegetable","House"
            })
            {
                if(context.Products.SingleOrDefault(x => x.ProductName == name) == null)
                {
                    context.Products.Add(new Models.Product() { ProductName = name });

                    context.SaveChanges();
                }
            }
        }
    }
}
