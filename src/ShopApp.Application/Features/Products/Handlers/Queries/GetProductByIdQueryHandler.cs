using MediatR;
using Serilog;
using ShopApp.Application.Features.Products.Queries;
using ShopApp.Core.Entities;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Handlers.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
            {
                Log.Warning("Product with Id {ProductId} not found", request.Id);
                throw new KeyNotFoundException("Product not found");
            }

            Log.Information("Product with Id {ProductId} retrieved successfully", request.Id);

            return product;
        }
    }
}
