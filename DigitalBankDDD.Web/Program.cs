using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Mapper;
using DigitalBankDDD.Application.Services;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Domain.Services;
using DigitalBankDDD.Infra.Database;
using DigitalBankDDD.Infra.Repositories;
using DigitalBankDDD.Infra.Utils;
using DigitalBankDDD.Web.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtSettings = builder.Configuration.GetSection("JwtSettings");


//Infraestructure
builder.Services.AddDbContext<BankContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Application
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Domain
builder.Services.AddScoped<IAccountDomainService, AccountDomainService>();

//Handlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Utils
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();
DatabaseConnectionTester.TestConnection(dbContext);

app.MapControllers(); 

app.UseExceptionHandler();

app.Run();