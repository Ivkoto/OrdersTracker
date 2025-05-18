using Microsoft.AspNetCore.Mvc;
using OrdersTracker.API.Extensions;
using OrdersTracker.API.Repositories;

namespace OrdersTracker.API.Endpoints;

public static class Customers
{
    public static void MapCustomers(this WebApplication app)
    {
        app.MapGet("/customers", GetCustomersList);
        app.MapGet("/customers/{id}", GetCustomerById);
        app.MapGet("/customers/{id}/orders", GetCustomerOrders);
    }

    private static async Task<IResult> GetCustomersList(HttpContext context, [FromServices] ICustomersRepository customersRepository)
    {
        var result = await customersRepository.GetCustomersList(context.RequestAborted);

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

        if (result == null)
            return Results.NotFound($"Customer with ID {id} not found.");

        return Results.Ok(result);
    }
}
