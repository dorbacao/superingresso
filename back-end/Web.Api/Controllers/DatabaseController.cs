using Microsoft.AspNetCore.Mvc;
using Web.Api.Database;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(ILogger<DatabaseController> logger, SuperIngressoContext context)
        {
            _logger = logger;
            Context = context;
        }

        public SuperIngressoContext Context { get; }

        [HttpGet]
        public IActionResult DropAndCreate()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            return Ok();
        }
    }
}