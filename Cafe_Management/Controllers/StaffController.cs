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
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;
        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> Staff_ID,
                                                Nullable<bool> IsActive,
                                                Nullable<int> StaffGroup_ID,
                                                string Username,
                                                string Password,
                                                string Staff_Phone)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _staffService.Get(Staff_ID,
                                                 IsActive,
                                                StaffGroup_ID,
                                                Username,
                                                Password,
                                                Staff_Phone);


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
        public async Task<IActionResult> AddMenu([FromBody] Staff staff)
        {
            APIResult result = new APIResult();
            try
            {
                if (staff.StaffGroup_ID == null)
                {
                    result.Status = 0;
                    result.Message = "StaffGroup_ID can not be empty";
                    return BadRequest(result);
                }
                if (staff.Staff_FullName == null)
                {
                    result.Status = 0;
                    result.Message = "Staff_FullName can not be empty";
                    return BadRequest(result);
                }
                if (staff.Username == null)
                {
                    result.Status = 0;
                    result.Message = "Username can not be empty";
                    return BadRequest(result);
                }
                if (staff.Password == null)
                {
                    result.Status = 0;
                    result.Message = "Password can not be empty";
                    return BadRequest(result);
                }

                await _staffService.Create(staff);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(Get), new { id = staff.Staff_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Staff staff)
        {
            APIResult result = new APIResult();
            try
            {
                if (staff.Staff_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Staff_ID cannot be empty";
                    return BadRequest(result);
                }
                await _staffService.Update(staff);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
        [HttpGet("username")]
        public async Task<IActionResult>GetStaffByUserName(string userName)
        {
            try
            {
                var staff = await _staffService.getStaffByUserName(userName);
                APIResult result = new APIResult();
                if (staff != null)
                {
                    result = new APIResult
                    {
                        Data = staff,
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
                        Message = "Successful",
                        Status = 200
                    };
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 200
                });
            }
        }
        [HttpGet("staff_id")]
        public async Task<IActionResult>getStaffById(int id)
        {
            try
            {
                var staff = await _staffService.getStaffById(id);
                APIResult result = new APIResult();
                if (staff != null)
                {
                    result = new APIResult
                    {
                        Data = staff,
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
                        Message = "successful",
                        Status = 200
                    };
                    return Ok(result);
                }
            }catch(Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.InnerException?.Message,
                    Status = 400
                });
            }
        }

    }
}
