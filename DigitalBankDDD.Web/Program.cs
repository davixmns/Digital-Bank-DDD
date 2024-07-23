using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Services;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Domain.Services;
using DigitalBankDDD.Infra.Database;
using DigitalBankDDD.Infra.UnitOfWork;
using DigitalBankDDD.Infra.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Infraestructure
builder.Services.AddDbContext<BankContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Application
builder.Services.AddScoped<IAccountService, AccountService>();

//Domain
builder.Services.AddScoped<IAccountDomainService, AccountDomainService>();

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

app.MapGet("/", () => "Hello World!");

app.Run();