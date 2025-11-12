using MediatR;
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
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<List<Product>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"products:category:{request.Category}";

            var cachedProducts = await _cacheService.GetAsync<List<Product>>(cacheKey);
            if (cachedProducts != null)
                return cachedProducts;

            var products = await _productRepository.GetByCategoryAsync(request.Category, cancellationToken);

            await _cacheService.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));

            return products;
        }
    }

}
