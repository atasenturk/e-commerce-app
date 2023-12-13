using AutoMapper;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Models.Queries;
using E_CommerceApp.Common.Models.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Api.Application.Features.Queries.User;

public class GetUserShoppingCartDetailQueryHandler : IRequestHandler<GetUserShoppingCartDetailQuery, GetUserShoppingCartDetailViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMapper _mapper;

    public GetUserShoppingCartDetailQueryHandler(IUserRepository userRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _mapper = mapper;
    }

    public async Task<GetUserShoppingCartDetailViewModel> Handle(GetUserShoppingCartDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.AsQueryable()
            .Include(q => q.ShoppingCart)
            .Where(q => q.Id.Equals(request.UserId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new DatabaseValidationException();
        }

        if (user.ShoppingCart == null)
        {
            var entity = new GetUserShoppingCartDetailViewModel
            {
                Id = request.UserId,
                ProductCount = 0,
                Products = new List<ProductViewModel>(),
                TotalPrice = 0
            };
            return entity;
        }

        var shoppingCartItems = _shoppingCartRepository.AsQueryable()
            .Include(q => q.ShoppingCartItems)
            .ThenInclude(q => q.Product)
            .Where(q => q.Id == user.ShoppingCartId)
            .Select(q => q.ShoppingCartItems).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (shoppingCartItems.Result == null)
        {
            throw new DatabaseValidationException("Products could not be found!");

        }
        var itemsList = shoppingCartItems.Result.ToList();
        var result = new GetUserShoppingCartDetailViewModel
        {
            Id = request.UserId,
            ProductCount = itemsList.Count,
            Products = new List<ProductViewModel>(),
            TotalPrice = itemsList.Sum(q => q.Product.Price * q.Quantity)
    };

        foreach (var item in itemsList.Select(cartItem => new ProductViewModel
                 {
                     ProductName = cartItem.Product.Name,
                     Price = cartItem.Product.Price,
                     Quantity = cartItem.Quantity,
                     Description = cartItem.Product.Description
                 }))
        {
            result.Products.Add(item);
        }

        return result;
    }
}