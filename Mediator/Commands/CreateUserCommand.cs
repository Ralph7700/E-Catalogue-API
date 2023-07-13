using AutoMapper;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Images;
using e_catalog_backend.Repositories.Users;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class CreateUserCommand : IRequest<ReadUserDto>
{
    public CreateUserDto CreateUserDto { get; set; }
    public Role role { get; set; }
    
    public CreateUserCommand(CreateUserDto createUserDto, Role role)
    {
        CreateUserDto = createUserDto;
        this.role = role;
    }
}
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,ReadUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
    }

    public async Task<ReadUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.CreateUserDto);
        user.Role = request.role;
        await _userRepository.CreateUser(user);
        
        if (request.CreateUserDto.Image != null)
        {
            var image = await _imageRepository.UploadUserImage(request.CreateUserDto.Image, user.UserId);
            user.PhotoUrl = image;
            await _userRepository.UpdateUser(user);
        }

        return _mapper.Map<ReadUserDto>(user);
    }
}

