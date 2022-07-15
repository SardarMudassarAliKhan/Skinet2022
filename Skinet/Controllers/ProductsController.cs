using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Dtos;
using Skinet.Errors;
using Skinet.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Controllers
{
  
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Products> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _productType;
        public IMapper _Mapper;

        public ProductsController(IGenericRepository<Products> productRepository,
            IGenericRepository<ProductBrand> BrandRepository, 
            IGenericRepository<ProductType> productType, IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = BrandRepository;
            _productType = productType;
            _Mapper = mapper;
        }
        [HttpGet(nameof(GetProducts))]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts(
            [FromQuery]ProductSpecPrams productSpecPrams)
        {
            var spec = new ProductWithSpecificationTypesAndBrand(productSpecPrams);
            var speccount = new ProductWithFilterCountSpecification(productSpecPrams);
            var total = await _productRepository.CountAsync(spec);
            var products = await _productRepository.ListAsync(spec);
            var data = _Mapper.Map<IReadOnlyList<Products>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(productSpecPrams.pageIndex, productSpecPrams.pageSize, total, data));
        }

        [HttpGet(nameof(GetProductById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponce),StatusCodes.Status404NotFound)]
        public async Task<ProductDto> GetProductById([FromQuery] int Id)
        {
            var spec = new ProductWithSpecificationTypesAndBrand(Id);
            var products = await _productRepository.GetByIdAsync(Id);
            return _Mapper.Map<ProductDto>(products);
        }

        [HttpGet(nameof(GetProductBrand))]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrand()
        {
            var obj = await _brandRepository.GetAllAsync();
            return Ok(obj);
        }

        [HttpGet(nameof(GetProductTypes))]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var obj = await _productRepository.GetAllAsync();
            return Ok(obj);
        }
    }
}

