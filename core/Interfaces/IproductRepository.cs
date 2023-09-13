using core.Entities;
using Core.Entities;

namespace core.Interfaces
{
  public interface IProductRepository
    {
        Task<Product>GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>>GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>>GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>>GetProductTypesAsync();
    }
}