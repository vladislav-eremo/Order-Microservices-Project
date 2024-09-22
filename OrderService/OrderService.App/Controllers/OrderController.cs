using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.DTOs;
using OrderService.Services.Order;

namespace OrderService.App.Controllers
{
    [Route("[controller]/[action]")]
    public class OrderController(IOrderService orderService) : Controller
    {
        [HttpGet]
        public ActionResult<List<OrderDto>> GetOrders()
        {
            return Ok(orderService.GetAllOrders());
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderDto order)
        {
            orderService.CreateOrder(order);
            return Ok("Заявка создана");
        }
    }
}
