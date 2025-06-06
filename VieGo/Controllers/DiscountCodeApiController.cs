using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Models;

namespace VieGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodeApiController : ControllerBase
    {
        private readonly IDiscountCodeService _service;

        public DiscountCodeApiController(IDiscountCodeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var voucher = _service.GetById(id);
            return voucher == null ? NotFound() : Ok(voucher);
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            return Ok(_service.GetByStatus(status));
        }

        [HttpPost]
        public IActionResult Create([FromBody] DiscountCode model)
        {
            _service.Add(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DiscountCode model)
        {
            model.DiscountCodeID = id;
            _service.Update(model);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpGet("validate")]
        public IActionResult ValidateDiscount([FromQuery] string code)
        {
            var discount = _service.GetByCode(code).FirstOrDefault(d => d.Code == code && d.Status == "Active"); // hoặc query DB
            if (discount == null || discount.IsExpired)
            {
                return NotFound(new { valid = false });
            }

            return Ok(new { valid = true, percentage = discount.DiscountValue });
        }
    }
}
