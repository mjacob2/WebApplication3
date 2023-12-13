using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;

namespace Tests
{
    public class OrderRepositoryTests
    {
        private MyECommerceContext GetInMemoryDbContext()
        {
            // Create a new DbContextOptionsBuilder with InMemory database provider
            var options = new DbContextOptionsBuilder<MyECommerceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name to ensure a fresh database
                .Options;

            // Create an instance of MyECommerceContext with the options
            var dbContext = new MyECommerceContext(options);

            // Ensure the database is created
            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreateNewOrder_WithOrderDetails()
        {
            // Arrange
            using var context = GetInMemoryDbContext(); // Create InMemory context
            var repository = new OrderRepository(context); // Create the repository with the context
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { ProductId = 1, Quantity = 2, UnitPrice = 10m },
                new OrderDetail { ProductId = 2, Quantity = 1, UnitPrice = 20m }
            };

            // Act
            var order = await repository.CreateOrderAsync(orderDetails);

            // Assert
            Assert.NotNull(order);
            Assert.NotEqual(0, order.OrderId); // Ensure a non-zero Id is generated
            Assert.Equal(DateTime.UtcNow.Date, order.OrderDate.Date); // Assert that the date matches today's date
            Assert.Equal(2, order.OrderDetails.Count); // Verify that there are two order details

            // Additional assertions as needed...
        }
    }
}