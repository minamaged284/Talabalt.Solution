using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabalt.APIS.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.Specefications;

namespace Talabalt.APIS.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Products> _products;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Products> products,IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAll()
        {
            var spec = new ProductSpecifications();
            //var products = await _products.GetAllAsync();
            var products = await  _products.GetAllBySpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(products));

        }


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

            return NotFound(new {Message="Not found",StatusCode=404});
        }

    }
}
