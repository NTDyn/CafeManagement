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
    public class HistoryDiscountController : ControllerBase
    {
        private readonly HistoryDiscountService _historyDiscountService;
        public HistoryDiscountController(HistoryDiscountService historyDiscountService)
        {
            _historyDiscountService = historyDiscountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHistoryDiscounts(Nullable<int> History_ID, Nullable<int> Customer_ID)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _historyDiscountService.GetHistoryDiscounts(History_ID, Customer_ID);


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
        public async Task<IActionResult> AddHistoryDiscount([FromBody] HistoryDisscount historyDisscount)
        {
            APIResult result = new APIResult();
            try
            {
                if (historyDisscount.Customer_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Customer ID can not be empty";
                    return BadRequest(result);
                }
                if (historyDisscount.Receipt_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Receipt ID can not be empty";
                    return BadRequest(result);
                }


                await _historyDiscountService.AddHistoryDiscount(historyDisscount);
                result.Data = historyDisscount;
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(AddHistoryDiscount), new { id = historyDisscount.History_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHistoryDiscount([FromBody] HistoryDisscount historyDisscount)
        {
            APIResult result = new APIResult();
            try
            {
                if (historyDisscount.History_ID == null)
                {
                    result.Status = 0;
                    result.Message = "History_ID cannot be empty";
                    return BadRequest(result);
                }
                await _historyDiscountService.UpdateHistoryDiscount(historyDisscount);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            var cus = await _historyDiscountService.GetHistoryDiscounts(historyDisscount.History_ID, null);
            result.Data = cus;
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
    }
}
