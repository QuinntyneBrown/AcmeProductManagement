using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Acme.Api.Core;
using Acme.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Api.Features
{
    public class GetProductById
    {
        public class Request: IRequest<Response>
        {
            public Guid ProductId { get; set; }
        }

        public class Response: ResponseBase
        {
            public ProductDto Product { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IAcmeDbContext _context;
        
            public Handler(IAcmeDbContext context)
                => _context = context;
        
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new () {
                    Product = (await _context.Products.SingleOrDefaultAsync(x => x.ProductId == request.ProductId)).ToDto()
                };
            }
            
        }
    }
}
