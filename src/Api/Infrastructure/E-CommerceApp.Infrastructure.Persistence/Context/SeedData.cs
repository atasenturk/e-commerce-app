using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_CommerceApp.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        //private static List<User> GetUsers()
        //{
        //    var result = new Faker<User>("tr")
        //        .RuleFor(q => q.Id, q => new Guid())
        //        .RuleFor(q => q.CreatedDate, q => q.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
        //        .RuleFor(q => q.FirstName, q => q.Person.FirstName)
        //        .RuleFor(q => q.LastName, q => q.Person.LastName)
        //        .RuleFor(q => q.Address, q => $"{q.Person.Address.City}, {q.Person.Address.Street}")
        //        .RuleFor(q => q.Email, q => q.Internet.Email())
        //        .RuleFor(q => q.Password, q => PasswordEncryptor.Encrypt(q.Internet.Password()))
        //        .RuleFor(q => q.UserName, q => q.Internet.UserName())
        //        .Generate(100);

        //    return result;
        //}

        //private static List<Product> GetProducts()
        //{
        //    var result = new Faker<Product>("tr")
        //        .RuleFor(q => q.Id, q => new Guid())
        //        .RuleFor(q => q.CreatedDate, q => q.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
        //        .RuleFor(q => q.Name, q => q.Commerce.ProductName())
        //        .RuleFor(q => q.Description, q => q.Commerce.ProductDescription())
        //        .RuleFor(q => q.Price, q => q.Random.Double(10, 100))
        //        .Generate(50);

        //    return result;
        //}


        private static List<Product> GetProducts()
        {
            var products = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.CreatedDate, f => f.Date.Between(DateTime.Now.AddDays(-365), DateTime.Now))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Double(1, 1000))
                .Generate(50); // 50 adet ürün oluştur

            return products;
        }

        private static List<User> GetUsers(List<Product> products)
        {
            var users = new Faker<User>("tr")
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.CreatedDate, f => f.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Address, f => $"{f.Person.Address.City}, {f.Person.Address.Street}")
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => PasswordEncryptor.Encrypt(f.Internet.Password()))
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .Generate(100);

            foreach (var user in users)
            {
                if (new Random().NextDouble() > 0.5) // Yarıya yakın user'a shopping cart ekleyelim
                {
                    var cart = CreateShoppingCart(user.Id, products);
                    user.ShoppingCart = cart;
                    user.ShoppingCartId = cart.Id;
                }
            }

            return users;
        }

        private static ShoppingCart CreateShoppingCart(Guid userId, List<Product> products)
        {
            var cart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UserId = userId,
                ShoppingCartItems = GetShoppingCartItems(products)
            };

            return cart;
        }

        private static ICollection<ShoppingCartItem> GetShoppingCartItems(List<Product> products)
        {
            var shoppingCartItems = new Faker<ShoppingCartItem>()
                .RuleFor(sci => sci.Id, f => Guid.NewGuid())
                .RuleFor(sci => sci.CreatedDate, f => f.Date.Recent())
                .RuleFor(sci => sci.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(sci => sci.ProductId, f => f.PickRandom(products).Id)
                .Generate(new Random().Next(1, 5));

            return shoppingCartItems;
        }


        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration.GetConnectionString("ECommerceConnStr"));

            var context = new ECommerceDbContext(dbContextBuilder.Options);


            var products = GetProducts();
            await context.Products.AddRangeAsync(products);

            var users = SeedData.GetUsers(products);
            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }

    }
}
