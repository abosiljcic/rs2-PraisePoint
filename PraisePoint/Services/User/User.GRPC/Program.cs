using User.GRPC.Protos;
using User.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<int, GetPointsResponse>().ReverseMap();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PointsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
