using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Management.Controllers
{
    public class StoreIngedientController:ControllerBase
    {
        private readonly StoreIngredientService _storeIngredientService;
        public StoreIngedientController(StoreIngredientService storeIngredientService)
        {
            _storeIngredientService = storeIngredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> Ingredient_ID = null, Nullable<int> Warehouse_ID = null)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _storeIngredientService.Get(Ingredient_ID, Warehouse_ID);
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
    }
}
