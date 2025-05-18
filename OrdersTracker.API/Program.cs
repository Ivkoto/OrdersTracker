using OrdersTracker.API.Endpoints;
using OrdersTracker.API.Extensions;
using OrdersTracker.API.Infrastructure;
using OrdersTracker.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AllowedOriginsPolicy(corsOrigins!);

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCustomers();
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.Run();
