using AutoMapper;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Users;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetAllSalesmenQuery : IRequest<List<ReadUserDto>>
{
    
}

public class GetAllSalesmenQueryHandler : IRequestHandler<GetAllSalesmenQuery, List<ReadUserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllSalesmenQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadUserDto>> Handle(GetAllSalesmenQuery request, CancellationToken cancellationToken)
    {
        var salesmen = await _userRepository.GetAllSalesmen();
        return _mapper.Map<List<ReadUserDto>>(salesmen);
    }
}
