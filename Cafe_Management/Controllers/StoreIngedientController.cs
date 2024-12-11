using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class StoreIngedientController:ControllerBase
    {
        private readonly StoreIngredientService _storeIngredientService;
        private readonly AppDbContext _context;
        public StoreIngedientController(StoreIngredientService storeIngredientService, AppDbContext context)
        {
            _storeIngredientService = storeIngredientService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Nullable<int> Ingredient_ID = null, Nullable<int> Warehouse_ID = null)
        {
            APIResult result = new APIResult();
            try
            {
                var data = await _storeIngredientService.Get(Ingredient_ID, Warehouse_ID);
                var dataBatch = await _context.BatchRecipe.Select(x => new CombinedData
                {
                    Type = "Batch",
                    Quantity = (double)x.Quality,
                    Unit = (int)x.Unit,
                    Ingredient_ID = (int)x.IngredientResult_ID,
                    CreatedDate = (DateTime)x.CreatedDate
                }).ToListAsync();
                var dataSubBatch = await _context.BatchRecipeDetail.Select(x => new CombinedData
                {
                    Type = "SubBatch",
                    Quantity = (double)x.Quality,
                    Unit = (int)x.Unit,
                    Ingredient_ID = (int)x.Ingredient_ID,
                    CreatedDate = (DateTime)x.CreatedDate
                }).ToListAsync();
                var dataSpoiled = await _context.SpoiledIngredientDetail.Select(x => new CombinedData
                {
                    Type = "Spoiled",
                    Quantity = (double)x.Quality,
                    Unit = (int)x.Unit,
                    Ingredient_ID = (int)x.Ingredient_ID,
                    CreatedDate = (DateTime)x.CreatedDate
                }).ToListAsync();

               var dataPO = await _context.IngredientSupplierDetail.Select(x => new CombinedData
               {
                   Type = "Purchase Order",
                   Quantity = (double)x.Quality,
                   Unit = (int)x.Unit,
                   Ingredient_ID = (int)x.Ingredient_ID,
                   CreatedDate = (DateTime)x.CreatedDate
               }).ToListAsync();

                var combinedData = dataBatch
                                    .Concat(dataSubBatch)
                                    .Concat(dataSpoiled)
                                    .Concat(dataPO)
                                    .OrderByDescending(x => x.CreatedDate)
                                    .ToList();
                var JoinData = (from store in data 
                                join cb in combinedData on store.Ingredient_ID equals cb.Ingredient_ID 
                                into his
                                select new
                                {
                                    Ingredient_ID = store.Ingredient_ID,
                                    Price = store.Price,
                                    Quality = store.Quality,
                                    Report = his
                                }).ToList();
                if (data != null)
                {
                    result.Data = JoinData;
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
