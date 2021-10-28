using DSCC._7417.DAL.DBO;
using DSCC._7417.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DSCC._7417.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // the abstract class will be inherited by product and category controllers
    public abstract class GenericController<T> : ControllerBase where T : BaseEntity
    {
        // Generic repository that will accept a model class
        protected readonly IRepository<T> _repository;

        protected GenericController(IRepository<T> repository)
        {
            _repository = repository;
        }
    }
}
