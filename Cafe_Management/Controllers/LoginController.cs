using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<APIResult> Login(string Username, string Password)
        {
            APIResult result = new APIResult();
            try
            {
                if (Username == null || Password == null) 
                {
                    result.Status = 0;
                    result.Message = "Username & Password cannot be empty!";
                    return result;
                }
                Staff staff = await _context.Staff.Where(x=>x.Username == Username && x.Password == Password && x.IsActive == true).SingleOrDefaultAsync();
                if(staff == null)
                {
                    result.Status = 0;
                    result.Message = "Username & Password khong chinh xac!";
                    return result;
                }
                StaffGroup staffGroup = await _context.StaffGroup.Where(x => x.StaffGroup_ID == staff.StaffGroup_ID).SingleOrDefaultAsync();

                List<StaffGroupLinkPermission> permissions = null;

                if (staffGroup != null) 
                {
                    permissions = await _context.StaffGroupLinkPermission.Where(x=>x.StaffGroup == staffGroup.StaffGroup_ID && x.IsActive == true).ToListAsync();
                }
                var Data = new
                {
                    Staff_FullName = staff.Staff_FullName,
                    Staff_UserName=staff.Username,
                    StaffGroup_Name = staffGroup != null ? staffGroup.StaffGroup_Name : null,
                    permissions = permissions.Count() > 0 ? permissions.Select( x => new {
                        Permission_ID = x.Permission_ID
                    }).ToList() : null,

                };
                result.Message = "Successfully";
                result.Status = 200;
                result.Data = Data;
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return result;
            }
            return result;
        }
        [HttpPost("user")]
        public async Task<IActionResult>LoginUser(string userName,string password)
        {
            try
            {
                APIResult result = new APIResult();
                if (userName == null || password == null)
                {
                    result = new APIResult
                    {
                        Status = 0,
                        Message = "username and password are not empty"
                    };
                    return Ok(result);
                }
                else
                {
                    Customer cus =await _context.Customer.Where(c => c.Username == userName && c.Password == password).SingleOrDefaultAsync();
                    if (cus == null)
                    {
                        result = new APIResult
                        {
                            Message = "Username or password incorrect",
                            Status=0
                        };
                        return Ok(result);
                    }
                    else
                    {
                        if (cus.IsActive==false)
                        {
                            result = new APIResult
                            {
                                Message = "Account is not working",
                                Status = 0
                            };
                            return Ok(result);
                        }
                        else
                        {
                            result = new APIResult
                            {
                                Message = "Login Successful",
                                Data = cus,
                                Status = 200
                            };
                            return Ok(result);
                        }
                    }
                   
                }
            }
            catch
            {
                return BadRequest(new APIResult
                {
                    Message = "Something went wrong",
                    Status = 400
                });
            }
        }
    }
}
