namespace WebApplication3
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyECommerceContext _context;

        public OrderRepository(MyECommerceContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(List<OrderDetail> orderDetails)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                OrderDetails = orderDetails
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        // Implement other methods specific to the Order repository
    }
}
