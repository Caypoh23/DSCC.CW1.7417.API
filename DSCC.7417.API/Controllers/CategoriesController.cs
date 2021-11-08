using DSCC._7417.DAL.DBO;
using DSCC._7417.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC._7417.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IRepository<Category> categoriesRepository) : base(categoriesRepository)
        {

        }

        /// <summary>
        /// Gets the list of all Categories.
        /// </summary>
        /// <returns>The list of Categories.</returns>
        /// <response code="200">List of categories are successfully returned</response>
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            // Get all the categories
            var categories = await _repository.GetAllAsync();
            return new OkObjectResult(categories);
        }


        /// <summary>
        /// Gets a single category details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single category</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Categories/id
        ///     {
        ///        "id": {id}
        ///     }
        ///
        /// </remarks>
        /// <returns>a Particular Category details</returns>
        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return new OkObjectResult (category);
        }

        /// <summary>
        /// Edits a Category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns>A modified category</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Categories/id
        ///     {
        ///        "id": {id},
        ///        "CategoryName": "Modified Category Name",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the edited category</response>
        /// <response code="400">If the category is null</response>
        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            // check if id is correct
            if (id != category.Id)
            {
                return BadRequest();
            }

            // validation for editing a category
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _repository.UpdateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
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
        /// Creates a Category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>A newly created category</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Categories
        ///     {
        ///        "id": {id},
        ///        "CategoryName": "Category Name"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">If the category is null</response>
        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Removes a category from Database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Categories/id
        ///     {
        ///        "id": {id}
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the deleted Category</response>
        /// <response code="400">If the category is null</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(category);

            return category;
        }
    }
}
