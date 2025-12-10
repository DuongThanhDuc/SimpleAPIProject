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
                return NotFound("Order doesn't exist.");
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult AddOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null.");
            }

            if(order.ID <= 0)
            {
                return BadRequest("Order ID cannot be below 0.");
            }

               if(order.UserID <= 0)
            {
                return BadRequest("User ID cannot be below 0.");
            }

            if (order.TotalAmount < 0)
            {
                return BadRequest("TotalAmount must be a positive value.");
            }

            var allowedStatus = new[] { "Processing", "Completed", "Cancelled" };
            if (!allowedStatus.Contains(order.Status))
            {
                return BadRequest("Invalid status value. Status must either be Processing, Completed, or Cancelled.");
            }


            _orderServices.AddOrder(order);
            return CreatedAtAction(nameof(GetOrderByID), new { id = order.ID }, order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            var existingOrder = _orderServices.GetOrderByID(id);
            if (existingOrder == null)
            {
                return NotFound("Order doesn't exist");
            }

            if(order.UserID <= 0)
            {
                return BadRequest("User ID cannot be below or 0.");
            }

            if (order.TotalAmount < 0)
            {
                return BadRequest("TotalAmount must be a positive value.");
            }

            var allowedStatus = new[] { "Processing", "Completed", "Cancelled" };
            if (!allowedStatus.Contains(order.Status))
            {
                return BadRequest("Invalid status value. Status must either be Processing, Completed, or Cancelled.");
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
                return NotFound("Order doesn't exist");
            }

            _orderServices.DeleteOrder(id);
            return NoContent();
        }
    }
}
