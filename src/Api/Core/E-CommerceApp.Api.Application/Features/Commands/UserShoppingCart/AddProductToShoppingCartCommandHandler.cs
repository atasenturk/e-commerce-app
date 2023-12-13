using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Infrastructure.Extensions;
using E_CommerceApp.Common.Models.Queries;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Api.Application.Features.Commands.UserShoppingCart
{
    internal class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, AddProductToShoppingCartViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public AddProductToShoppingCartCommandHandler(IUserRepository userRepository, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<AddProductToShoppingCartViewModel> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.AsQueryable()
                .Where(q => q.Id == request.UserId)
                .Include(q => q.ShoppingCart)
                .ThenInclude(q => q.ShoppingCartItems)
                .FirstOrDefault();

            if (user == null)
            {
                throw new DatabaseValidationException("User not found!");
            }

            var product = await _productRepository.GetAsync(request.ProductId);
            if (product == null)
            {
                throw new DatabaseValidationException("Product not found!");
            }

            ShoppingCart? userShoppingCart = user.ShoppingCart;

            var value = new AddProductToShoppingCartViewModel
            {
                Count = 1,
                ProductId = product.Id,
                UserId = user.Id
            };
            if (userShoppingCart == null)
            {
                var cart = CreateShoppingCart(user.Id);
                cart.UserId = user.Id;

                ShoppingCartItem itemInCart = new ShoppingCartItem
                {
                    ShoppingCart = user.ShoppingCart,
                    ShoppingCartId = cart.Id,
                    Quantity = 1,
                    Id = new Guid(),
                    Product = product,
                };
                cart.ShoppingCartItems.Add(itemInCart);
                await _shoppingCartRepository.AddAsync(cart);

                user.ShoppingCart = cart;
                user.ShoppingCartId = cart.Id;
                //user.ShoppingCart.ShoppingCartItems.Add(itemInCart);
                _userRepository.UpdateAsync(user);
            }
            else
            {
                var shoppingCartContainsItem = userShoppingCart.ShoppingCartItems.Any(q => q.ProductId == request.ProductId);
                if (shoppingCartContainsItem)
                {
                    var itemInCart = userShoppingCart.ShoppingCartItems.FirstOrDefault(q => q.ProductId == request.ProductId);
                    itemInCart.Quantity++;
                    value.Count = itemInCart.Quantity;
                }
                else
                {
                    ShoppingCartItem itemInCart = new ShoppingCartItem
                    {
                        ShoppingCart = userShoppingCart,
                        Quantity = 1,
                        Id = new Guid(),
                        Product = product,
                    };

                    userShoppingCart.ShoppingCartItems.Add(itemInCart);
                    _shoppingCartRepository.UpdateAsync(userShoppingCart);
                }
            }
            await _userRepository.SaveChangesAsync();
            await _shoppingCartRepository.SaveChangesAsync();

            return value;
        }

        private static ShoppingCart CreateShoppingCart(Guid userId)
        {
            var cart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UserId = userId,
                ShoppingCartItems = new List<ShoppingCartItem>()
            };
            return cart;
        }
    }
}
