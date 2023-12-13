using AutoMapper;
using E_CommerceApp.Api.Application.Interfaces.Repositories;
using E_CommerceApp.Common.Models.Queries.User;
using MediatR;

namespace E_CommerceApp.Api.Application.Features.Queries.User;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<GetUserViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<GetUserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        var mapping = _mapper.Map<List<GetUserViewModel>>(users) ?? throw new ArgumentNullException("There are no users!");

        return mapping;
    }
}