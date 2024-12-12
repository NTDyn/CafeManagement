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
    public class CupponController :ControllerBase
    {
        private readonly CupponService _cupponService;
        public CupponController(CupponService cupponService)
        {
            _cupponService = cupponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCuppons(Nullable<int> Cuppon_ID, Nullable<bool> IsActive)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _cupponService.GetCuppons(Cuppon_ID, IsActive);


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
        public async Task<IActionResult> AddCuppon([FromBody] Cuppon cuppon)
        {
            APIResult result = new APIResult();
            try
            {
                if (cuppon.Cuppon_Name == null)
                {
                    result.Status = 0;
                    result.Message = "Cuppon Name can not be empty";
                    return BadRequest(result);
                }
                if (cuppon.Disscount == null)
                {
                    result.Status = 0;
                    result.Message = "Disscount can not be empty";
                    return BadRequest(result);
                }
                if (cuppon.Cuppon_Type == null)
                {
                    result.Status = 0;
                    result.Message = "Cuppon type can not be empty";
                    return BadRequest(result);
                }
                if (cuppon.ApplyLevel_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Apply level id can not be empty";
                    return BadRequest(result);
                }
                if (cuppon.DateStart == null)
                {
                    result.Status = 0;
                    result.Message = "Datestart can not be empty";
                    return BadRequest(result);
                }
                if (cuppon.DateEnd == null)
                {
                    result.Status = 0;
                    result.Message = "Date end can not be empty";
                    return BadRequest(result);
                }
                await _cupponService.AddCuppon(cuppon);
                result.Data = cuppon;
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(AddCuppon), new { id = cuppon.Cuppon_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCuppon([FromBody] Cuppon cuppon)
        {
            APIResult result = new APIResult();
            try
            {
                if (cuppon.Cuppon_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Cuppon_ID cannot be empty";
                    return BadRequest(result);
                }
                await _cupponService.UpdateCuppon(cuppon);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            var cus = await _cupponService.GetCuppons(cuppon.Cuppon_ID, null);
            result.Data = cus;
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
    }
}
