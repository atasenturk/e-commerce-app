using E_CommerceApp.Api.Application.Features.Queries.User;
using E_CommerceApp.Common.Models.Queries.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Common.Models.Queries.Product;
using AutoMapper;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;

namespace E_CommerceApp.Api.Application.Features.Queries.Product
{
    internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductItemListViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductItemListViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var mapping = _mapper.Map<List<ProductItemListViewModel>>(products) ?? throw new ArgumentNullException("There are no products!");
            return mapping;
        }
    }
}
