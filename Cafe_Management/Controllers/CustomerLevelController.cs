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
    public class CustomerLevelController : ControllerBase 
    {
        private readonly CustomerLevelService _levelService;
        public CustomerLevelController(CustomerLevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerLevels(Nullable<int> Level_ID, Nullable<bool> IsActive)
        {
            APIResult result = new APIResult();
            try
            {
                var data =  await _levelService.GetCustomerLevels(Level_ID, IsActive);


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
        public async Task<IActionResult> AddCustomerLevel([FromBody] CustomerLevel customerLevel)
        {
            try
            {
                if (customerLevel.Level_Name == null)
                {
                    return BadRequest("Customer name can not be empty");
                }

                 await _levelService.AddCustomerLevel(customerLevel);
                APIResult result = new APIResult
                {
                    Data = customerLevel,
                    Message = "Successfully added the ingredient",
                    Status = 200
                };
                return CreatedAtAction(nameof(AddCustomerLevel), new { id = customerLevel.Level_ID }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerLevel([FromBody] CustomerLevel customerLevel)
        {

            try
            {
                if (customerLevel.Level_ID == null)
                {
                    return BadRequest("Customer level id can not be empty");
                }
                await _levelService.UpdateCustomerLevel(customerLevel);
                var result = await _levelService.GetCustomerLevels(customerLevel.Level_ID, null);
                if (result.Count() == 0)
                {
                    return BadRequest("Customer Level id does not exist");
                }
                APIResult response = new APIResult
                {
                    Data = result,
                    Message = "Successfully",
                    Status = 200
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
