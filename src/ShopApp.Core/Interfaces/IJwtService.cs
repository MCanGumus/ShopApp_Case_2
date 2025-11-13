using ShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Core.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
