using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Acme.Api.Extensions;
using Acme.Api.Core;
using Acme.Api.Interfaces;
using Acme.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Acme.Api.Features
{
    public class GetProductsPage
    {
        public class Request: IRequest<Response>
        {
            public int PageSize { get; set; }
            public int Index { get; set; }
        }

        public class Response: ResponseBase
        {
            public int Length { get; set; }
            public List<ProductDto> Entities { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IAcmeDbContext _context;
        
            public Handler(IAcmeDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = from product in _context.Products
                    select product;
                
                var length = await _context.Products.CountAsync();
                
                var products = await query.Page(request.Index, request.PageSize)
                    .Select(x => x.ToDto()).ToListAsync();
                
                return new()
                {
                    Length = length,
                    Entities = products
                };
            }
            
        }
    }
}
