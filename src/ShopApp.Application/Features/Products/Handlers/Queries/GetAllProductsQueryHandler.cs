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
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICacheService _cacheService;

        public GetAllProductsQueryHandler(IProductRepository productRepository, ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "products:all";

            var cachedProducts = await _cacheService.GetAsync<List<Product>>(cacheKey);
            if (cachedProducts != null)
                return cachedProducts;

            var products = await _productRepository.GetAllAsync(cancellationToken);

            await _cacheService.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));

            return products;
        }
    }
}
