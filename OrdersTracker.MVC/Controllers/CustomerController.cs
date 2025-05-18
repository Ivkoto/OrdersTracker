using Microsoft.AspNetCore.Mvc;
using OrdersTracker.MVC.Models;
using OrdersTracker.MVC.Services;

namespace OrdersTracker.MVC.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> Index(string searchTerm = "")
    {
        var customers = await _customerService.GetCustomers();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            customers = customers
                .Where(c => c.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                         || c.CustomerID.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return View(customers);
    }

    public async Task<IActionResult> Details(string id)
    {
        var customerTask =  _customerService.GetCustomerById(id);
        var ordersTask =  _customerService.GetCustomerOrders(id);

        await Task.WhenAll(customerTask, ordersTask);

        var customer = await customerTask;
        var orders = await ordersTask;

        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        var viewModel = new CustomerDetailsViewModel
        {
            Customer = customer,
            Orders = orders
        };

        return View(viewModel);
    }
}
