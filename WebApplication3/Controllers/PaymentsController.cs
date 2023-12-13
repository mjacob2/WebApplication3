using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken, decimal amount)
        {
            var charge = await _paymentService.ProcessPaymentAsync(stripeToken, amount);

            if (charge.Status == "succeeded")
            {
                // The charge was successful, do any post-payment processing here
                return Ok();
            }
            else
            {
                // The charge failed, do any error handling here
                return BadRequest();
            }
        }
    }
}
