using System;
using Acme.Api.Models;

namespace Acme.Api.Features
{
    public static class ProductExtensions
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName
            };
        }
    }
}
