using AutoMapper;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Users;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class ValidateUserQuery : IRequest<ReadUserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public ValidateUserQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, ReadUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ValidateUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ReadUserDto> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.ValidateUser(request.Email, request.Password);
        if (user == null) return null;
        return _mapper.Map<ReadUserDto>(user);
    }
}