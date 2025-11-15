using MediatR;
using Serilog;
using ShopApp.Application.Features.Products.Commands;
using ShopApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;

        public DeleteProductCommandHandler(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingProduct == null)
            {
                Log.Warning("Product with Id {ProductId} not found", request.Id);
                throw new KeyNotFoundException("Product not found"); // Global handler yakalar
            }

            await _productRepository.DeleteAsync(request.Id, cancellationToken);

            await _cacheService.RemoveAsync($"products:category:{existingProduct.Category}");
            await _cacheService.RemoveAsync("products:all");

            Log.Information("Product with Id {ProductId} deleted successfully", request.Id);

        }
    }

}
