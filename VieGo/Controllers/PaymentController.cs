using Business.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace VieGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PayOSService _payOSService;

        public PaymentController(PayOSService payOSService)
        {
            _payOSService = payOSService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestModel request)
        {
            try
            {
                var checkoutUrl = await _payOSService.CreatePaymentLink(
                    amount: request.Amount,
                    orderCode: request.OrderCode,
                    description: request.Description,
                    buyerName: request.BuyerName,
                    returnUrl: request.ReturnUrl,
                    cancelUrl: request.CancelUrl,
                    signature: request.Signature
                );

                return Ok(new { checkoutUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }


    public class PaymentRequestModel
    {
        public decimal Amount { get; set; }
        public int OrderCode { get; set; }
        public string Description { get; set; }
        public string BuyerName { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string Signature { get; set; }
    }

}
