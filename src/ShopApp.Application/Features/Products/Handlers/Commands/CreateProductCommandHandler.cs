using MediatR;
using ShopApp.Application.Features.Products.Commands;
using ShopApp.Core.Entities;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _repository;
        private readonly ICacheService _cacheService;

        public CreateProductCommandHandler(IProductRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                Category = request.Category,
                StockQuantity = request.StockQuantity
            };

            var addedProduct = await _repository.AddAsync(product, cancellationToken);

            await _cacheService.RemoveAsync($"products:category:{product.Category}"); 
            await _cacheService.RemoveAsync("products:all");

            return addedProduct;
        }
    }
}
