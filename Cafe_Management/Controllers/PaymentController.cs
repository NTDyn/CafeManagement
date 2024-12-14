using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Model;
using Cafe_Management.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
      private readonly VNpayService _vnpayService;
        public PaymentController(VNpayService vnpayService)
        {
            _vnpayService = vnpayService;
        }
        [HttpPost("create-payment") ]
        public IActionResult CreatePayment([FromBody] PaymentRequestDto paymentRequest)
        {
            try
            {
                var paymentUrl = _vnpayService.CreatePaymentUrl(paymentRequest.Amount, paymentRequest.OrderId);
                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("return")]
        public IActionResult PaymentReturn(IQueryCollection query)
        {
            var vnp_ResponseCode = query["vnp_ResponseCode"];
            var vnp_TxnRef = query["vnp_TxnRef"];

            if (vnp_ResponseCode == "00")
            {
                // Thanh toán thành công
                return Ok("Thanh toán thành công");
            }
            else
            {
                // Thanh toán thất bại
                return Ok("Thanh toán thất bại");
            }
        }

    }
}

