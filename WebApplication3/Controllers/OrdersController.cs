using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PlaceOrder(List<OrderDetail> orderDetails)
        {
            var order = await _orderRepository.CreateOrderAsync(orderDetails);
            if (order == null)
            {
                return BadRequest();
            }

            return Ok(order);
        }
    }
}
