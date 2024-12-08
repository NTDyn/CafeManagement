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
    public class SpoiledIngredientController:ControllerBase
    {
        private readonly SpoiledIngredientService _spoiledIngredientService;
        private readonly IngredientService _ingredientService;
        public SpoiledIngredientController(SpoiledIngredientService spoiledIngredientService, IngredientService ingredientService)
        {
            _spoiledIngredientService = spoiledIngredientService;
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> Spoiled_ID = null)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _spoiledIngredientService.Get(Spoiled_ID);
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
        public async Task<IActionResult> Create([FromBody] SpoiledIngredient SpoiledIngredient)
        {
            APIResult result = new APIResult();
            try
            {
                if (SpoiledIngredient.Staff_ID == null)
                {
                    result.Status = 0;
                    result.Message = "Staff_ID cannot be empty";
                    return BadRequest();
                }
                if (SpoiledIngredient.Details == null || SpoiledIngredient.Details.Count == 0)
                {
                    result.Status = 0;
                    result.Message = "Details cannot be empty";
                    return BadRequest();
                }
                else
                {
                    foreach (var item in SpoiledIngredient.Details)
                    {
                        var Data = await _ingredientService.GetIngredientById(item.Ingredient_ID);
                        if (Data == null)
                        {
                            result.Status = 0;
                            result.Message = $"Ingredient ID = {item.Ingredient_ID} does not exits";
                            return BadRequest();
                        }
                    }
                }
                await _spoiledIngredientService.Create(SpoiledIngredient);
                result.Status = 200;
                result.Message = "Successfully";
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.Message;
            }

            return CreatedAtAction(nameof(Get), new { id = SpoiledIngredient.Spoiled_ID }, result);
        }
    }
}
