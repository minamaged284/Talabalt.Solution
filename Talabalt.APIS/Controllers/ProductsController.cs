using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabalt.APIS.DTOS;
using Talabalt.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.Specefications;

namespace Talabalt.APIS.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Products> _products;
        private readonly IGenericRepository<ProductBrand> _brands;
        private readonly IGenericRepository<ProductCategory> _categories;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Products> products, IGenericRepository<ProductBrand> brands, IGenericRepository<ProductCategory> categories,IMapper mapper)
        {
            _products = products;
            _brands = brands;
            _categories = categories;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAll()
        {
            var spec = new ProductSpecifications();
            //var products = await _products.GetAllAsync();
            var products = await  _products.GetAllBySpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(products));

        }

        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Get(int id)
        {
            //var product = await  _products.GetByIdAsync(id);
            var spec = new ProductSpecifications(id);
            var product = await _products.GetBySpecAsync(spec);


            if (product is not null)
            {
                return Ok(_mapper.Map<Products,ProductDTO>(product));
            }

            return NotFound(new ApiResponse(404));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<ProductBrand>> GetCategories()
        {
            var categories = await _categories.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrandss()
        {
            var brands = await _brands.GetAllAsync();
            return Ok(brands);
        }



    }
}
