using DSCC._7417.DAL.DBO;
using DSCC._7417.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC._7417.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        private readonly IRepository<Category> _categoryRepository;

        public ProductsController(IRepository<Product> productsRepository, IRepository<Category> categoryRepository) : base(productsRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            // gets all products in the table
            var products = await _repository.GetAllAsync(nameof(Product.ProductCategory));
            return new OkObjectResult(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id, nameof(Product.ProductCategory));

            if (product == null)
            {
                return NotFound();
            }
            // returns a particular product
            return new OkObjectResult(product);
        }

        // populate dropdown list with category data for mvc
        [HttpGet("Categories")]
        public async Task<IActionResult> GetGenres()
        {
            //get all genres 
            var genres = await _categoryRepository.GetAllAsync();
            return new OkObjectResult(genres);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            // Validation for editing a product
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _repository.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if the product does not exists it will throw an error
                if (!_repository.IfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id, nameof(Product.ProductCategory));

            if (product == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(product);

            return product;
        }
    }
}
