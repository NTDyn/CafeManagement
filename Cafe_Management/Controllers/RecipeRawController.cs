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
    public class RecipeRawController :ControllerBase
    {
        private readonly RecipeRawService _recipeRawService;
        public RecipeRawController(RecipeRawService recipeRawService)
        {
            _recipeRawService = recipeRawService;
        }

        [HttpGet]
        public async Task<IActionResult> GetrecipeRaw(Nullable<int> ingredientResult)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _recipeRawService.GetRecipeRaw(ingredientResult);


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
        public async Task<IActionResult> AddRecipeRaw([FromBody] RecipeRaw recipeRaw)
        {
            APIResult result = new APIResult();
            try
            {
                if (recipeRaw.Ingredient_Result == null)
                {
                    result.Status = 0;
                    result.Message = "ingredient result can not be empty";
                    return BadRequest(result);
                }
                if (recipeRaw.Ingredient_Raw == null)
                {
                    result.Status = 0;
                    result.Message = "ingredient raw can not be empty";
                    return BadRequest(result);
                }
                if (recipeRaw.Quantity == null)
                {
                    result.Status = 0;
                    result.Message = "Quantity can not be empty";
                    return BadRequest(result);
                }
                if (recipeRaw.Unit == null)
                {
                    result.Status = 0;
                    result.Message = "unit can not be empty";
                    return BadRequest(result);
                }

                await _recipeRawService.AddRecipeRaw(recipeRaw);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(AddRecipeRaw), new { id = recipeRaw.Recipe_ID }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipeRaw([FromBody] RecipeRaw recipeRaw)
        {
            APIResult result = new APIResult();
            try
            {
                if (recipeRaw.Recipe_ID == null)
                {
                    result.Status = 0;
                    result.Message = "recipeRaw_ID cannot be empty";
                    return BadRequest(result);
                }
                await _recipeRawService.UpdateRecipeRaw(recipeRaw);
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            var cus = await _recipeRawService.GetRecipeRaw(recipeRaw.Ingredient_Result);
            result.Data = cus;
            result.Status = 200;
            result.Message = "Successfully";
            return Ok(result);
        }
    }
}
