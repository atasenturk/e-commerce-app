using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Infrastructure.Persistence.Context;

namespace E_CommerceApp.Infrastructure.Persistence.Repositories;

public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ECommerceDbContext context) : base(context)
    {
    }
}