using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.DTOs;
using OrderService.Services.Order;

namespace OrderService.App.Controllers
{
    [Route("[controller]/[action]")]
    public class OrderController(IOrderService orderService) : Controller
    {
        [HttpGet]
        public ActionResult<List<GetOrderDto>> GetOrders()
        {
            return Ok(orderService.GetAllOrders());
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderDto order)
        {
            orderService.CreateOrder(order);
            return Ok("Заявка создана");
        }
    }
}
