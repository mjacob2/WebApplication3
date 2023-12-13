using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3;

namespace Tests
{
    public class RepositoryTests
    {
        private MyECommerceContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MyECommerceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new MyECommerceContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            using var context = GetInMemoryDbContext();
            var repository = new Repository<Product>(context);

            // Assuming Product is a class that has already been defined
            var products = new List<Product>
        {
            new Product { Name = "test", Price = 100, },
            new Product { Name = "test2", Price = 200, }
        };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.Equal(products.Count, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenItExists()
        {
            using var context = GetInMemoryDbContext();
            var repository = new Repository<Product>(context);

            var product = new Product { Name = "test", Price = 100 };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(product.ProductId); // Replace with the primary key name

            Assert.Equal(product, result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewEntity()
        {
            using var context = GetInMemoryDbContext();
            var repository = new Repository<Product>(context);

            // Create a new product and set all required properties
            var product = new Product
            {
                // Assuming 'Name' is a required property of your 'Product' entity
                Name = "Test Product Name",
                Price = 100,
            };

            // Attempting to add the product to the database
            var result = await repository.CreateAsync(product);

            Assert.Equal(product, result); // Assert that the same instance was returned
            Assert.NotNull(await context.Products.FindAsync(product.ProductId)); // Assuming 'Id' is the primary key
                                                                                 // Verify other properties if necessary
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_WhenItExists()
        {
            using var context = GetInMemoryDbContext();
            var repository = new Repository<Product>(context);

            var product = new Product {Name = "test", Price = 100 };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            /* Change some properties of the `product` */

            await repository.UpdateAsync(product);

            var updatedEntry = await context.Products.FindAsync(product.ProductId); // Replace with the primary key name
            /* Assert that the properties are updated */
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity_WhenItExists()
        {
            using var context = GetInMemoryDbContext();
            var repository = new Repository<Product>(context);

            var product = new Product { Name = "test", Price = 100, };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(product.ProductId); // Replace with the primary key name

            Assert.Null(await context.Products.FindAsync(product.ProductId)); // Replace with the primary key name
        }
    }
}
