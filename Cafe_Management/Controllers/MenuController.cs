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
            APIResult result = new APIResult();
            try
            {
                if (menu.Menu_Name == null)
                {
                    result.Status = 0;
                    result.Message = "Menu Name can not be empty";
                    return BadRequest(result);
                }
                

                await _menuService.AddMenu(menu);
                result.Data = menu;
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(AddMenu), new { id = menu.Menu_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu([FromBody] Menu menu)
        {
            APIResult result = new APIResult();
            try
            {
                if (menu.Menu_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Menu_ID cannot be empty";
                    return BadRequest(result);
                }
                await _menuService.UpdateMenu(menu);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            var cus = await _menuService.GetMenus(menu.Menu_ID, null);
            result.Data = cus;
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
    }
}
