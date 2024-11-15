using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Business;
using Order.Shared.Dto;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IBusiness business, ILogger<OrderController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllOrders")]
        [Authorize]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            return Ok(_business.GetOrders());
        }

        [HttpGet(Name = "GetOrder")]
        [Authorize]
        public ActionResult<OrderDto>? GetOrder(int id)
        {
            return Ok(_business.GetOrder(id));
        }

        [HttpPost(Name = "AddNewOrder")]
        [Authorize]
        public ActionResult<OrderDto> AddNewOrder(OrderInsertDto order)
        {
            return Ok(_business.AddOrder(order));
        }

        [HttpGet("/Order/GetOrders", Name = "GetOrders")]
        [Authorize]
        public ActionResult<List<OrderInfoDto>> GetOrders(string status = "",  DateTime from = default, DateTime to = default) 
        {
            string userId = User.Identity.Name;
            return Ok(_business.GetOrderOfUser(userId, status, from, to));
        }

        [HttpPut(Name = "UpdateOrder")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrderDto> UpdateOrder(int orderId, OrderDto order)
        {
            return Ok(_business.UpdateOrder(orderId, order));
        }

        [HttpPut(Name = "UpdateOrderStatus")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrderDto> UpdateOrderStatus(int orderId, string status)
        {
            return Ok(_business.UpdateOrderStatus(orderId, status));
        }

        [HttpDelete(Name = "DeleteOrder")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOrder(int orderId)
        {
            _business.DeleteOrder(orderId);
            return Ok(orderId + " has been successfully deleted");
        }
    }
}
