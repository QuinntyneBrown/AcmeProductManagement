using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Acme.Api.Core;
using Acme.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Api.Features
{
    public class GetProducts
    {
        public class Request: IRequest<Response> { }

        public class Response: ResponseBase
        {
            public List<ProductDto> Products { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IAcmeDbContext _context;
        
            public Handler(IAcmeDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Products = await _context.Products.Select(x => x.ToDto()).ToListAsync()
                };
            }
            
        }
    }
}
