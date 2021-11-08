using DSCC._7417.DAL.DBO;
using DSCC._7417.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC._7417.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : GenericController<Product>
    {
        private readonly IRepository<Category> _categoryRepository;

        public ProductsController(IRepository<Product> productsRepository, IRepository<Category> categoryRepository) : base(productsRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Gets the list of all Products.
        /// </summary>
        /// <returns>The list of Products.</returns>
        /// <response code="200">List of Products are successfully returned</response>
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            // gets all products in the table
            var products = await _repository.GetAllAsync(nameof(Product.ProductCategory));
            return new OkObjectResult(products);
        }

        /// <summary>
        /// Gets a single product details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Products/id
        ///     {
        ///        "id": {id}
        ///     }
        ///
        /// </remarks>
        /// <returns>a Particular product details</returns>
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            // get a particular product including its category
            var product = await _repository.GetByIdAsync(id, nameof(Product.ProductCategory));

            if (product == null)
            {
                return NotFound();
            }
            
            return new OkObjectResult(product);
        }

        /// <summary>
        /// Gets the list of all Categories.
        /// </summary>
        /// <returns>The list of Categories.</returns>
        /// <response code="200">If the Categories were not found</response>
        // populate dropdown list with categories data for mvc
        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return new OkObjectResult(categories);
        }

        /// <summary>
        /// Edits a Product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>A modified product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Products/id
        ///     {
        ///        "id": {id},
        ///        "ProductName": "Product Name",
        ///        "Description": "Modified Product Description",
        ///        "CategoryId": "{categoryId}",
        ///        "Price": "5.99"
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the edited product</response>
        /// <response code="400">If the product is null</response>
        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            // check if id is correct

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

        /// <summary>
        /// Create a new Product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A newly created product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Products
        ///     {
        ///        "ProductName": "Product Name",
        ///        "Description": "Product Description",
        ///        "CategoryId": "{categoryId}",
        ///        "Price": "5.99"
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns a newly created product</response>
        /// <response code="400">If the product is null</response>
        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        /// <summary>
        /// Removes a product from Database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Products/id
        ///     {
        ///        "id": {id}
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the deleted product</response>
        /// <response code="400">If the product is null</response>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
