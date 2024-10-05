using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;

namespace Talabalt.APIS.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Products> _products;

        public ProductsController(IGenericRepository<Products> products)
        {
            _products = products;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAll()
        {

            var products = await _products.GetAllAsync();
            return Ok(products);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Get(int id)
        {
            var product = await  _products.GetByIdAsync(id);

            if (product is not null)
            {
                return Ok(product);
            }

            return NotFound(new {Message="Not found",StatusCode=404});
        }

    }
}
