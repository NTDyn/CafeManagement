using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(Nullable<int> Customer_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> level_ID)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _customerService.GetCustomers(Customer_ID,IsActive,level_ID);


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
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            APIResult result = new APIResult();
            try
            {
                if (customer.Customer_Name == null)
                {
                    result.Status = 0;
                    result.Message = "Customer Name can not be empty";
                    return BadRequest(result);
                }
                if (customer.Customer_Phone == null)
                {
                    result.Status = 0;
                    result.Message = "Customer phone can not be empty";
                    return BadRequest(result);
                }
               

                await _customerService.AddCustomer(customer);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(AddCustomer), new { id = customer.Customer_Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            APIResult result = new APIResult();
            try
            {
                if (customer.Customer_Id == null)
                {
                    result.Status = 0;
                    result.Message = "Customer_ID cannot be empty";
                    return BadRequest(result);
                }
                await _customerService.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            var cus = await _customerService.GetCustomers(customer.Customer_Id, null, null);
            result.Data = cus;
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
        [HttpGet("userName")]
        public async Task<IActionResult>getCustomerByUserName(string userName)
        {
            try
            {
                var cus = await _customerService.getCustomerByUserName(userName);
                APIResult result = new APIResult();
                if (cus != null)
                {
                    result = new APIResult
                    {
                        Data = cus,
                        Status = 200,
                        Message = "Success"
                    };
                    return Ok(result);
                }
                else
                {
                    result = new APIResult
                    {
                        Data = null,
                        Message = "Not Found",
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
