﻿using Microsoft.AspNetCore.Mvc;
using OrderProcessingAPI.Models;

namespace OrderProcessingAPI.Controllers
{
    public class OrderProcessingAPI
    {
    }
    // this defines the route for the API and marks it as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // this is the Endpoint to process an order
        [HttpPost("process")]
        public ActionResult<OrderResponse> ProcessOrder([FromBody] OrderRequest orderRequest)
        {
            // Validating the request
            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
            {
                return BadRequest("Invalid order request.");
            }

            // Calculating subtotal (sum of item prices)
            decimal subtotal = orderRequest.Items.Sum(item => item.Price);
            decimal tax = subtotal * 0.13m; // ontario tax rate
            decimal total = subtotal + tax;

            // response
            var response = new OrderResponse
            {
                OrderId = orderRequest.OrderId,
                Items = orderRequest.Items,
                Subtotal = subtotal,
                Total = total
            };

            return Ok(response);
        }
    }


}
