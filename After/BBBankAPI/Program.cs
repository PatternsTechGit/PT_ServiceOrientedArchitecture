using Infrastructure;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddSingleton<BBBankContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
