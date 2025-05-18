using Microsoft.AspNetCore.Mvc;
using OrdersTracker.API.Extensions;
using OrdersTracker.API.Repositories;

namespace OrdersTracker.API.Endpoints;

public static class Customers
{
    public static void MapCustomers(this WebApplication app)
    {
        app.MapGet("/customers", GetCustomersList)
            .RequireCors("AllowedOriginsPolicy");

        app.MapGet("/customers/{id}", GetCustomerById)
            .RequireCors("AllowedOriginsPolicy");

        app.MapGet("/customers/{id}/orders", GetCustomerOrders)
            .RequireCors("AllowedOriginsPolicy");
    }

    private static async Task<IResult> GetCustomersList(HttpContext context, [FromServices] ICustomersRepository customersRepository)
    {
        var result = await customersRepository.GetCustomersList(context.RequestAborted);

        if (result == null)
            return Results.NotFound($"No customers found in the Database.");

        var customers = result.Select(c => c.ToResponse()).ToList();

        return Results.Ok(customers);
    }

    private static async Task<IResult> GetCustomerById(string id, HttpContext context, [FromServices] ICustomersRepository customersRepository)
    {
        var result = await customersRepository.GetCustomerById(id, context.RequestAborted);
        
        if (result == null)
            return Results.NotFound($"Customer with ID {id} not found.");

        var currentCustomer = result.ToResponse();

        return Results.Ok(currentCustomer);
    }

    private static async Task<IResult> GetCustomerOrders(string id, HttpContext context, [FromServices] ICustomersRepository customersRepository)
    {
        var result = await customersRepository.GetCustomerOrders(id, context.RequestAborted);

        if (result == null || !result.Any())
            return Results.NotFound($"There are no orders for the current customer.");

        var orders = result.Select(o => o.ToResponse()).ToList();

        return Results.Ok(orders);
    }
}
