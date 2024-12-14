﻿using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController:ControllerBase
    {
        private readonly ReceiptService _receiptService;
        private readonly ProductService _productService;
        public ReceiptController(ReceiptService receiptService, ProductService productService)
        {
            _receiptService = receiptService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> Receipt_ID = null)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _receiptService.Get(Receipt_ID);
                if (data != null)
                {
                    result.Data = data;
                    result.Message = "Successfully";
                    result.Status = 200;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = 0;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Receipt Receipt)
        {
            APIResult result = new APIResult();
            try
            {
                if (Receipt.Staff_ID == null)
                {

                    result.Status = 0;
                    result.Message = "Staff_ID cannot be empty";
                    return BadRequest();
                }
                if(Receipt.Details == null || Receipt.Details.Count == 0)
                {
                    result.Status = 0;
                    result.Message = "Details cannot be empty";
                    return BadRequest();
                }
                else
                {
                    foreach (var item in Receipt.Details) { 
                        var Product = await _productService.GetProductByIdAsync(item.Product_ID);
                        if (Product == null) {
                            result.Status = 0;
                            result.Message = $"Product ID = {item.Product_ID} does not exits";
                            return BadRequest();
                        }
                    }
                }
                await _receiptService.Create(Receipt);
                result.Data = Receipt;
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(Get), new { id = Receipt.Receipt_ID }, result);
        }

        [HttpPost("createCheckout")]
        public async Task<IActionResult> createReceiptCheckout(Receipt receipt)
        {
            try
            {
                await _receiptService.CreateReceiptCheckout(receipt);
                return Ok(new APIResult
                {
                    Message = "successful",
                    Status = 200
                });
            }catch(Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
        }
        [HttpPost("addCart")]
        public async Task<IActionResult> addCart([FromBody] AddCartDto addCart)
        {
            try
            {
                await _receiptService.AddCart(addCart.receiptDetail, addCart.id_customer);
                return Ok(new APIResult
                {
                    Message = "successful",
                    Status = 200
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
        }
        [HttpGet("getcart")]
        public async Task<IActionResult> getCart( int id)
        {
            try
            {
                APIResult result = new APIResult();
                var listCart = await _receiptService.getCartofCustomer(id);
                if (listCart != null)
                {
                    result = new APIResult
                    {
                        Data = listCart,
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
                else
                {
                    result = new APIResult
                    {
                        Data = null,
                        Message = "Cart is Empty",
                        Status = 200
                    };
                    return Ok(result);
                }
            }catch(Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
        }

    }
}
