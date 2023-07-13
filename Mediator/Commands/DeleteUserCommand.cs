using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Users;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class DeleteUserCommand : IRequest
{
    public string ManagerId { get; set; }
    
    public DeleteUserCommand(string managerId)
    {
        ManagerId = managerId;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUser(Guid.Parse(request.ManagerId));

        return Unit.Value;
    }
}