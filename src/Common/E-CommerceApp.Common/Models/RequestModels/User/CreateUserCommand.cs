using E_CommerceApp.Common.Models.Queries.User;
using MediatR;

namespace E_CommerceApp.Common.Models.RequestModels.User;

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