using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace VieGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourApiController : ControllerBase
    {
        private readonly ITourService _service;
        private readonly ILogger<TourApiController> _logger;

        public TourApiController(ITourService service, ILogger<TourApiController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var tours = _service.GetAll();
                return Ok(tours);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tours");
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var tour = _service.GetById(id);
                return tour == null ? NotFound(new { message = "Tour not found" }) : Ok(tour);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tour by id: {Id}", id);
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            try
            {
                var tours = _service.GetByStatus(status);
                return Ok(tours);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tours by status: {Status}", status);
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("serviceprovider/{serviceProviderId}")]
        public IActionResult GetByServiceProvider(int serviceProviderId)
        {
            try
            {
                var tours = _service.GetByServiceProvider(serviceProviderId);
                return Ok(tours);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tours by service provider: {ServiceProviderId}", serviceProviderId);
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tour model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _service.Add(model);
                return CreatedAtAction(nameof(GetById), new { id = model.TourId }, model);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid tour data provided");
                return BadRequest(new { error = "Invalid data", message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tour");
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Tour model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.TourId = id;
                _service.Update(model);
                return Ok(model);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid tour data provided for update");
                return BadRequest(new { error = "Invalid data", message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tour: {Id}", id);
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingTour = _service.GetById(id);
                if (existingTour == null)
                {
                    return NotFound(new { message = "Tour not found" });
                }

                _service.Delete(id);
                return Ok(new { message = "Tour deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tour: {Id}", id);
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        [HttpGet("test-db")]
        public IActionResult TestDatabase()
        {
            try
            {
                var tours = _service.GetAll();
                var count = tours.Count();
                return Ok(new
                {
                    message = "Database connection successful",
                    tourCount = count,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database connection test failed");
                return StatusCode(500, new
                {
                    error = "Database connection failed",
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("session")]
        public IActionResult GetSession()
        {
            var email = HttpContext.Session.GetString("Email");
            var fullName = HttpContext.Session.GetString("FullName");
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (string.IsNullOrEmpty(email))
            {
                return Ok(new { IsLoggedIn = false });
            }

            return Ok(new
            {
                IsLoggedIn = true,
                UserId = userId,
                RoleId = roleId,
                Email = email,
                FullName = fullName
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return Ok(new { Message = "Logged out successfully" });
        }
    }
}