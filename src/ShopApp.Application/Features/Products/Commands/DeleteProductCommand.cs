using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}
