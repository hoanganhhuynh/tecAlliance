using Application.Employee.Commands;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterDependencyServices(typeof(CreateEmployeeCommandHandler).Assembly);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterRepositories();
builder.Services.AddConfigurationOptions(builder.Configuration);
builder.Services.AddCorsPolicies();

//var myPolicy = "myPolicy";
//builder.Services.AddCors(option => option.AddPolicy(name: myPolicy, policy =>
//{
//    policy.WithOrigins("http://localhost:4200/").AllowAnyMethod();
//}));

var app = builder.Build();

//app.UseCors(myPolicy);
app.ConfigureWebApp();

app.Run();

