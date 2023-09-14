
using API.Dtos;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>>  GetProducts()
        {
            var spec = new ProductsWithTypeAndBrandsSpecification();

            var products = await _ProductsRepo.ListAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
            
        }
         [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypeAndBrandsSpecification(id);
            
            var Product= await _ProductsRepo.GetEntityWithSpec(spec);

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