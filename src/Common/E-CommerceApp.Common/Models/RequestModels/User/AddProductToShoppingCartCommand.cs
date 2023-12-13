using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.Queries;
using MediatR;

namespace E_CommerceApp.Common.Models.RequestModels.User
{
    public class AddProductToShoppingCartCommand : IRequest<AddProductToShoppingCartViewModel>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
