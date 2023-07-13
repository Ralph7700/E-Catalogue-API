using AutoMapper;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Images;
using e_catalog_backend.Repositories.Users;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class UpdateUserCommand : IRequest<ReadUserDto>
{
    public UpdateUserDto UpdateUserDto { get; set; }
    public string? TargetId { get; set; }
    
    public UpdateUserCommand(UpdateUserDto updateUserDto, string? targetId)
    {
        UpdateUserDto = updateUserDto;
        TargetId = targetId;
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,ReadUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IImageRepository imageRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<ReadUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // get the user

        var user = await _userRepository.GetUserById(Guid.Parse(request.TargetId!));
        
        // update the user
        user.FirstName = request.UpdateUserDto.FirstName ?? user.FirstName;
        user.LastName = request.UpdateUserDto.LastName ?? user.LastName;
        user.Email = request.UpdateUserDto.Email ?? user.Email;
        user.PhoneNumber = request.UpdateUserDto.PhoneNumber ?? user.PhoneNumber;

        // update the user photo
        if (request.UpdateUserDto.Image != null)
        {
            var image = await _imageRepository.UploadUserImage(request.UpdateUserDto.Image, user.UserId);
            user.PhotoUrl = image;
        }

        // update the user
        await _userRepository.UpdateUser(user);

        return _mapper.Map<ReadUserDto>(user);
    }
}
