using OrdersTracker.API.Endpoints;
using OrdersTracker.API.Infrastructure;
using OrdersTracker.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new RequireHttpsAttribute());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCustomers();

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.Run();
