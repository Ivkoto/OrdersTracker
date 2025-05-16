using Microsoft.AspNetCore.Mvc;
using OrdersTracker.MVC.Services;
using System.Threading.Tasks;

namespace OrdersTracker.MVC.Controllers
{
  public class CustomerController : Controller
  {
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
      _customerService = customerService;
    }

    public async Task<IActionResult> Index()
    {
      var customers = await _customerService.GetCustomersAsync();
      return View(customers);
    }
  }
}
