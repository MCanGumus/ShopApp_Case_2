using MediatR;
using ShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Product>;
}
