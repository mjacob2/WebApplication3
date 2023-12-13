using Stripe;

namespace WebApplication3
{
    public class StripePaymentService : IPaymentService
    {
        public StripePaymentService()
        {
            // Make sure to set your secret key in a configuration or environment variable
            StripeConfiguration.ApiKey = "Your_Stripe_Secret_key";
        }

        public async Task<Charge> ProcessPaymentAsync(string token, decimal amount)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe requires amount in cents/pence
                Currency = "usd", // use the appropriate currency code
                Description = "Your charge description here",
                Source = token // This is the Stripe Token created client-side
            };

            var chargeService = new ChargeService();

            try
            {
                // Create and charge the customer
                Charge charge = await chargeService.CreateAsync(chargeOptions);
                return charge;
            }
            catch (StripeException e)
            {
                // Handle any errors from the Stripe API here
                // Log the error
                throw;
            }
        }
    }
}
