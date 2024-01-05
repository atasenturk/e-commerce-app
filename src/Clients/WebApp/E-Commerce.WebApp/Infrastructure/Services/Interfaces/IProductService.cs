using E_CommerceApp.Common.Models.Queries.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductItemListViewModel>> GetProducts();
    }
}