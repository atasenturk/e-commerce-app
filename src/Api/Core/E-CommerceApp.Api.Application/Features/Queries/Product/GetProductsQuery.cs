using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.Queries.Product;
using MediatR;

namespace E_CommerceApp.Api.Application.Features.Queries.Product
{
    public class GetProductsQuery : IRequest<List<ProductItemListViewModel>>
    {
    }
}
