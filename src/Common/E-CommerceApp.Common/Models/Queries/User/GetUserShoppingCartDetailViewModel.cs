using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Api.Domain.Models;

namespace E_CommerceApp.Common.Models.Queries.User
{
    public class GetUserShoppingCartDetailViewModel
    {
        public Guid Id { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
