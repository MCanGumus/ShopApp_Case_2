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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found");

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.ImageUrl = request.ImageUrl;
            existingProduct.Category = request.Category;
            existingProduct.StockQuantity = request.StockQuantity;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            await _cacheService.RemoveAsync($"products:category:{existingProduct.Category}");
            await _cacheService.RemoveAsync("products:all");

            return existingProduct;
        }
    }
}
