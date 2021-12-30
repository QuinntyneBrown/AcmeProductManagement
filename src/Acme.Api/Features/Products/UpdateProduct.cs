using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Acme.Api.Core;
using Acme.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Api.Features
{
    public class UpdateProduct
    {
        public class Validator: AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Product).NotNull();
                RuleFor(request => request.Product).SetValidator(new ProductValidator());
            }
        
        }

        public class Request: IRequest<Response>
        {
            public ProductDto Product { get; set; }
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
                var product = await _context.Products.SingleAsync(x => x.ProductId == request.Product.ProductId);

                product.ProductName = request.Product.ProductName;

                await _context.SaveChangesAsync(cancellationToken);
                
                return new ()
                {
                    Product = product.ToDto()
                };
            }
            
        }
    }
}
