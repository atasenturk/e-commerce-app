using E_CommerceApp.Common.Models.Queries.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Common.Models.RequestModels.User
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public LoginUserCommand()
        {

        }
    }

    public class CreateUserCommand : IRequest<CreateUserViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public CreateUserCommand(string firstName, string lastName, string address, string password, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Password = password;
            Email = email;
        }

        public CreateUserCommand()
        {

        }
    }
}
