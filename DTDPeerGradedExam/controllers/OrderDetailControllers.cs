using Microsoft.AspNetCore.Mvc;
using services;
using models;

namespace controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailControllers : ControllerBase
    {
        private readonly OrderDetailServices _orderDetailServices;

        public OrderDetailControllers()
        {
            _orderDetailServices = new OrderDetailServices();
        }

        [HttpGet]
        public ActionResult<List<OrderDetail>> GetAllOrderDetails()
        {
            var orderDetails = _orderDetailServices.LoadOrderDetails();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetail> GetOrderDetailByID(int id)
        {
            var orderDetail = _orderDetailServices.GetOrderDetailByID(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public ActionResult AddOrderDetail([FromBody] OrderDetail orderDetail)
        {
            _orderDetailServices.AddOrderDetail(orderDetail);
            return CreatedAtAction(nameof(GetOrderDetailByID), new { id = orderDetail.ID }, orderDetail);
        }   

        [HttpPut("{id}")]
        public ActionResult UpdateOrderDetail(int id, [FromBody] OrderDetail orderDetail)
        {
            var existingOrderDetail = _orderDetailServices.GetOrderDetailByID(id);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            orderDetail.ID = id; 
            _orderDetailServices.UpdateOrderDetail(orderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrderDetail(int id)
        {
            var existingOrderDetail = _orderDetailServices.GetOrderDetailByID(id);
            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            _orderDetailServices.DeleteOrderDetail(id);
            return NoContent();
        }
    }
}