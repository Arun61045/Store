
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
    public class ProductsController : BaseApiController
    {



        private readonly IGenericRepository<Product> _ProductsRepo;
        private readonly IGenericRepository<ProductBrand> _ProductBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> ProductsRepo,
        IGenericRepository<ProductBrand> ProductBrandRepo,IGenericRepository<ProductType>
        ProductTypeRepo,IMapper mapper)
        {
            _mapper = mapper;
        _ProductBrandRepo = ProductBrandRepo;
      _productTypeRepo = ProductTypeRepo;
      _ProductsRepo = ProductsRepo;
           
            
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>>  GetProducts(
           [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypeAndBrandsSpecification(productParams);

            var CountSpec= new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _ProductsRepo.CountAsync(CountSpec);

            var products = await _ProductsRepo.ListAsync(spec);

            var data =_mapper
                  .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize,totalItems,data));
            
        }
         [HttpGet("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

         
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypeAndBrandsSpecification(id);
            
            var Product= await _ProductsRepo.GetEntityWithSpec(spec);

            if (Product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductToReturnDto>(Product);


        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
           return Ok(await _ProductBrandRepo.ListAllAsync());   
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
           return Ok(await _productTypeRepo.ListAllAsync());   
        }
    }
}