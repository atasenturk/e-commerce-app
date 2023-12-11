using AutoMapper;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Api.Application.Features.Queries.GetUserShoppingCart;

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
            throw new DatabaseValidationException("User not found!");
        }

        if (user.ShoppingCart == null)
        {
            var entity = new GetUserShoppingCartDetailViewModel
            {
                Id = request.UserId,
                ProductCount = 0,
                ShoppingCartItems = new List<ShoppingCartItem>()
            };
            return entity;
        }

        var products = _shoppingCartRepository.AsQueryable()
            .Include(q => q.ShoppingCartItems)
            .ThenInclude(q=>q.Product)
            .Where(q => q.Id == user.ShoppingCartId)
            .Select(q => q.ShoppingCartItems).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (products.Result == null)
        {
            throw new DatabaseValidationException("Products could not be found!");

        }
        var result = new GetUserShoppingCartDetailViewModel
        {
            Id = request.UserId,
            ProductCount = products.Result.Count,
            ShoppingCartItems = products.Result.ToList()
        };

        return result;
    }
}