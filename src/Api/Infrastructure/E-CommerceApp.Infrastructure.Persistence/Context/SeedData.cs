using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_CommerceApp.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(q => q.Id, q => new Guid())
                .RuleFor(q => q.CreatedDate, q => q.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(q => q.FirstName, q => q.Person.FirstName)
                .RuleFor(q => q.LastName, q => q.Person.LastName)
                .RuleFor(q => q.Address, q => $"{q.Person.Address.City}, {q.Person.Address.Street}")
                .RuleFor(q => q.Email, q => q.Internet.Email())
                .RuleFor(q => q.Password, q => PasswordEncryptor.Encrypt(q.Internet.Password()))
                .RuleFor(q => q.UserName, q => q.Internet.UserName())
                .Generate(100);

            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["ECommerceConnStr"]);

            var context = new ECommerceDbContext(dbContextBuilder.Options);

            var users = GetUsers();
            var userIds = users.Select(q => q.Id);

            await context.Users.AddRangeAsync(users);

            

        }

    }
}
