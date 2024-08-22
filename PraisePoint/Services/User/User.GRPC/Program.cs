using User.GRPC.Protos;
using User.GRPC.Services;
using User.API.Services;
using Microsoft.EntityFrameworkCore;
using User.API.Data;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<UserContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")));
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<int, GetPointsResponse>().ReverseMap();
});

var app = builder.Build();
builder.Configuration.AddJsonFile("appsettings.grpc.json", optional: true, reloadOnChange: true);

// Configure the HTTP request pipeline.
app.MapGrpcService<PointsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
