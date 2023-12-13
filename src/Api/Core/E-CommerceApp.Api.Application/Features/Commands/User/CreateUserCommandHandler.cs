using AutoMapper;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Common.Infrastructure.Exceptions;
using E_CommerceApp.Common.Infrastructure.Extensions;
using E_CommerceApp.Common.Models.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;
using MediatR;

namespace E_CommerceApp.Api.Application.Features.Commands.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailAny = _userRepository.AsQueryable()
            .Any(q => q.Email == request.Email);

        if (emailAny)
        {
            throw new DatabaseDuplicateException($"Email address {request.Email} is already exists!");
        }

        var newEntity = _mapper.Map<Domain.Models.User>(request);

        newEntity.Password = PasswordEncryptor.Encrypt(newEntity.Password);
        newEntity.UserName = UsernameGenerator.GenerateUsername(newEntity.FirstName, newEntity.LastName);

        await _userRepository.AddAsync(newEntity);
        await _userRepository.SaveChangesAsync();

        return _mapper.Map<CreateUserViewModel>(newEntity);
    }
}