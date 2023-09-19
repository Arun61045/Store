using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class ProductRepository : IProductRepository
  {
    private readonly StoreContext _Context;
    public ProductRepository(StoreContext Context)
    {
      _Context = Context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
       return await _Context.ProductBrands.ToListAsync();    
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      return await _Context.Products
      .Include(p=>p.ProductType)
        .Include(p=>p.ProductBrand) 
      .FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
       

       
     return await _Context.Products
        .Include(p=>p.ProductType)
        .Include(p=>p.ProductBrand) 
         .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
       return await _Context.ProductTypes.ToListAsync();
    }

    
  }
}