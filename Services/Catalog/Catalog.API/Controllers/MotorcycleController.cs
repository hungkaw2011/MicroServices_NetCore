using Catalog.API.Entities.Vehicle;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        public MotorcycleController(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }
        [HttpGet]
        public IEnumerable<Motorcycle> GetAllMotorcycle()
        {
            return _motorcycleRepository.GetAll();
        }
    }
}
