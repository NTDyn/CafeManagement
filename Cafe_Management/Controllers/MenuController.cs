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
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus(Nullable<int> Menu_ID, Nullable<bool> IsActive)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _menuService.GetMenus(Menu_ID, IsActive);


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
        public async Task<IActionResult> AddMenu([FromBody] Menu menu)
        {
            try
            {
                if(menu.Menu_Name == null)
                {
                    return BadRequest("Menu name can not be empty");
                }

                await _menuService.AddMenu(menu);
                return CreatedAtAction(nameof(GetMenus), new { Menu_ID = menu.Menu_ID }, menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu([FromBody] Menu menu)
        {
            try
            {
                if (menu.Menu_ID == null)
                {
                    return BadRequest("Menu id can not be empty");
                }
                await _menuService.UpdateMenu(menu);
                var result = await _menuService.GetMenus(menu.Menu_ID, null);
                if(result.Count() == 0)
                {
                    return BadRequest("Menu id does not exist");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
