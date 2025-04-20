using OrderApp;
using System.Web.Http;
using System;

[RoutePrefix("api/orders")]
public class OrderController : ApiController
{
    private readonly OrderService _orderService;

    public OrderController()
    {
        _orderService = new OrderService(); // 实例化 OrderService
    }

    [HttpGet]
    [Route("")]
    public IHttpActionResult GetAllOrders()
    {
        // 返回所有订单（示例）
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult GetOrderById(int id)
    {
        var order = _orderService.GetOrder(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    [Route("")]
    public IHttpActionResult CreateOrder(Order order)
    {
        try
        {
            _orderService.AddOrder(order);
            return Ok("订单创建成功");
        }
        catch (Exception ex)
        {
            return BadRequest("创建失败：" + ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IHttpActionResult UpdateOrder(int id, Order order)
    {
        try
        {
            order.OrderId = id;
            _orderService.UpdateOrder(order);
            return Ok("订单更新成功");
        }
        catch (Exception ex)
        {
            return BadRequest("更新失败：" + ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IHttpActionResult DeleteOrder(int id)
    {
        try
        {
            _orderService.RemoveOrder(id);
            return Ok("订单删除成功");
        }
        catch (Exception ex)
        {
            return BadRequest("删除失败：" + ex.Message);
        }
    }
}
