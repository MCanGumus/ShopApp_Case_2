using MediatR;
using ShopApp.Core.Entities;
using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Queries
{
    public record GetProductsByCategoryQuery(Category Category) : IRequest<List<Product>>;
}
