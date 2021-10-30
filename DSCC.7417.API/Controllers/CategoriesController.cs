using DSCC._7417.DAL.DBO;
using DSCC._7417.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC._7417.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IRepository<Category> categoriesRepository) : base(categoriesRepository)
        {

        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _repository.GetAllAsync();
            return new OkObjectResult(categories);
        }

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

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
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

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

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
