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
    public class IngredientController : ControllerBase
    {
        private readonly IngredientService _ingredientService;

        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients(Nullable<int> Type = null)
        {
            var ingredients = await _ingredientService.GetAllIngredients(Type);
            if (ingredients != null && ingredients.Any()) // Check if products is not null and contains items
            {
                APIResult result = new APIResult
                {
                    Data = ingredients,
                    Message = "Successfully",
                    Status = 200
                };
                return Ok(result);
            }

            // If no products are found, return a NotFound or other relevant status
            return NotFound(new APIResult { Message = "No ingredients found", Status = 404 });

        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient([FromBody] Ingredient ingredient)
        {
            try
            {

                await _ingredientService.AddIngredient(ingredient);
                APIResult result = new APIResult
                {
                    Data = ingredient, // Set the added product as data
                    Message = "Successfully added the ingredient",
                    Status = 200
                };

                // Return the created result with the location of the new resource
                return CreatedAtAction(nameof(AddIngredient), new { id = ingredient.Ingredient_ID }, result);


            }
            catch (Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateIngredient([FromBody] Ingredient ingredient)
        {
            try
            {
                await _ingredientService.UpdateIngredient(ingredient);
                APIResult result = new APIResult
                {
                    Data = ingredient,
                    Message = "Successfully ",
                    Status = 200
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResult
                {
                    Message = ex.Message,
                    Status = 400
                });
            }
        }

    }
}
