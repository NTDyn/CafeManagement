using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    public class MenuDetailController:ControllerBase
    {
        private readonly MenuDetailService _menuDetail;
        public MenuDetailController(MenuDetailService menuDetail)
        {
            _menuDetail = menuDetail;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> menuId)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _menuDetail.Get(menuId);

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
        public async Task<IActionResult> Create([FromBody] List<MenuDetail> MenuDetails)
        {
            APIResult result = new APIResult();
            try
            {
                foreach(var menu in MenuDetails)
                {
                    if (menu.Menu_ID == null || menu.Product_ID == null)
                    {
                        result.Status = 0;
                        result.Message = "Menu_ID & Product_ID can not be empty";
                        return BadRequest(result);
                    }
                }
               

                await _menuDetail.Create(MenuDetails);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(Get), new { id = MenuDetails[0].Menu_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<MenuDetail> MenuDetails)
        {
            APIResult result = new APIResult();
            try
            {
                foreach (var menu in MenuDetails)
                {
                    if (menu.Menu_ID == null || menu.Product_ID == null)
                    {
                        result.Status = 0;
                        result.Message = "Menu_ID & Product_ID can not be empty";
                        return BadRequest(result);
                    }
                }
                await _menuDetail.Update(MenuDetails);
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
    }
}
