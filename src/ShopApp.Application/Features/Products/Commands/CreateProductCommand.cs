using MediatR;
using ShopApp.Core.Entities;
using ShopApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        Category Category,
        int StockQuantity
        ) : IRequest<Product>;
}
