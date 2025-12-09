using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using services;
using models;

namespace controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderControllers : ControllerBase
    {
        private readonly OrderServices _orderServices;

        public OrderControllers()
        {
            _orderServices = new OrderServices();
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
        {
            var orders = _orderServices.LoadOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderByID(int id)
        {
            var order = _orderServices.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult AddOrder([FromBody] Order order)
        {
            _orderServices.AddOrder(order);
            return CreatedAtAction(nameof(GetOrderByID), new { id = order.ID }, order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            var existingOrder = _orderServices.GetOrderByID(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            order.ID = id; 
            _orderServices.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var existingOrder = _orderServices.GetOrderByID(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _orderServices.DeleteOrder(id);
            return NoContent();
        }
    }
}
  