using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace core.Specifications
{
  public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
  {
    public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
    : base(x =>
         (string.IsNullOrEmpty(productParams.search)|| x.Name.ToLower().Contains
         (productParams.search))&&
         (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
         (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
       )
    {
    }
  }
}