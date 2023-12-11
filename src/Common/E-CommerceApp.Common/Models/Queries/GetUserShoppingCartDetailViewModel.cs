using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Api.Domain.Models;

namespace E_CommerceApp.Common.Models.Queries
{
    public class GetUserShoppingCartDetailViewModel
    {
        public Guid Id { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public int ProductCount { get; set; }
    }
}
