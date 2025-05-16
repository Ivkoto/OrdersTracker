using Microsoft.AspNetCore.Mvc;
using OrdersTracker.API.Endpoints;
using OrdersTracker.API.Infrastructure;

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
builder.Services.AddSingleton<IEndpoint, Customers>();

//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new RequireHttpsAttribute());
//});

var app = builder.Build();

// Call the DatabaseInitializer to ensure stored procedures exist
using (var scope = app.Services.CreateScope())
{
    await DatabaseInitializer.EnsureStoredProceduresAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Services.GetRequiredService<IEndpoint>().MapEndpoints(app);

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.Run();
