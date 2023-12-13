namespace WebApplication3
{
    public interface IPaymentService
    {
        Task<Stripe.Charge> ProcessPaymentAsync(string token, decimal amount);

    }
}
