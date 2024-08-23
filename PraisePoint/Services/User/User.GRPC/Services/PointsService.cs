using AutoMapper;
using Grpc.Core;
using User.API.Entities;
using User.API.Services;
using User.GRPC.Protos;

namespace User.GRPC.Services
{
    public class PointsService : PointsProtoService.PointsProtoServiceBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<PointsService> _logger;
        public PointsService(IUserService userService, IMapper mapper, ILogger<PointsService> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<GetPointsResponse> GetPoints(GetPointsRequest request, ServerCallContext context)
        {
            var company = await _userService.GetCompanyPointsNumber(Guid.Parse(request.CompanyId));

            if (company == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"PointsNumber for company = {request.CompanyId} is not found"));
            }

            _logger.LogInformation("PointsNumber is retrieved {pointsNumber}",
                company.PointsNumber);

            return _mapper.Map<GetPointsResponse>(company);
        }
    }
}
