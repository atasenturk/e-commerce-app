using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.Queries.User;
using MediatR;

namespace E_CommerceApp.Api.Application.Features.Queries.User
{
    public class GetUserQuery : IRequest<List<GetUserViewModel>>
    {
    }
}
