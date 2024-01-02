using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Enums;
using E_CommerceApp.Common.Models.Queries;
using MediatR;

namespace E_CommerceApp.Common.Models.RequestModels.User
{
    public class AddOrDeleteItemShoppingCartCommand : IRequest<UserShoppingCartItemViewModel>
    {
        public AddOrDeleteItemShoppingCartCommand(Guid userId, Guid productId, ActionType action)
        {
            UserId = userId;
            ProductId = productId;
            Action = action;
        }

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public ActionType Action { get; set; }

    }
}
