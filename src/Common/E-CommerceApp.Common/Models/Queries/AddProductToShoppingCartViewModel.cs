using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Models.Queries
{
    public class AddProductToShoppingCartViewModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
