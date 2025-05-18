using OrdersTracker.Contracts.Response;

namespace OrdersTracker.MVC.Models;

public class CustomerDetailsViewModel
{
    public CustomerDetailsResponse Customer { get; set; }
    public List<OrderResponse> Orders { get; set; }
}

