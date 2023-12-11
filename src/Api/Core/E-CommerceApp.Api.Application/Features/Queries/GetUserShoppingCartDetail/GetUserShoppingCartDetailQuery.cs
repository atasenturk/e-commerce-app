using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.Queries;
using MediatR;

namespace E_CommerceApp.Api.Application.Features.Queries.GetUserShoppingCart
{
    public class GetUserShoppingCartDetailQuery : IRequest<GetUserShoppingCartDetailViewModel>
    {
        public Guid UserId { get; set; }

        public GetUserShoppingCartDetailQuery(Guid userId)
        {
            UserId = userId;
        }
        public GetUserShoppingCartDetailQuery()
        {
            
        }
    }
}
