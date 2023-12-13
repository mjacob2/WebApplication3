namespace WebApplication3
{
    public interface IOrderRepository
    {
         Task<Order> CreateOrderAsync(List<OrderDetail> orderDetails);
        // Other methods specific to handling orders
    }
}
