using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using E_CommerceApp.Api.Domain.Models;
using Microsoft.Data.SqlClient;

namespace ECommerce.Projections.UserService.Services
{
    public class UserService
    {
        private readonly string connStr;

        public UserService(string connStr)
        {
            this.connStr = connStr;
        }

        public async Task CreateUser(User @event)
        {
            await using var connection = new SqlConnection(connStr);

            await connection.ExecuteAsync(
                "INSERT INTO Users(Id, FirstName, LastName, Address, UserName, Password, Email, CreatedDate, ShoppingCartId)" +
                "VALUES (@Id, @FirstName, @LastName, @Address, @UserName, @Password, @Email, @CreatedDate, @ShoppingCartId)",
                new User
                {
                    Id = @event.Id,
                    FirstName = @event.FirstName,
                    LastName = @event.LastName,
                    Address = @event.Address,
                    UserName = @event.UserName,
                    Password = @event.Password,
                    Email = @event.Email,
                    CreatedDate = DateTime.Now,
                    ShoppingCartId = new Guid(),
                }
            );

        }
    }
}
