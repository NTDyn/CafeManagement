using Cafe_Management.Application.Services;
using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.Odbc;

namespace Cafe_Management.Controllers
{
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _productCategoryService;
        private readonly IngredientService _ingredientService;

        public ProductController(ProductService productService, ProductCategoryService productCategoryService, IngredientService ingredientService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _ingredientService = ingredientService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            APIResult result = new APIResult();
            try
            {
                var products = await _productService.GetAllProductsAsync();


                if (products != null)
                {
                    result.Data = products;
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
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            APIResult result = new APIResult();

            try
            {
                var productCategorys = await _productCategoryService.GetAllProductCategories();
                var ingredients = await _ingredientService.GetAllIngredients();
                if (product.Product_Category == null)
                {
                    return BadRequest(new APIResult
                    {
                        Message = "Product_Category cannot be empty",
                        Status = 400
                    });
                }
                else
                {
                    int categoryCount = productCategorys.Where(x => x.Category_ID == product.Product_Category).Count();
                    if (categoryCount == 0)
                    {
                        return BadRequest(new APIResult
                        {
                            Message = "Product_Category does not exits",
                            Status = 400
                        });
                    }
                }
                if (product.ProductRecipe != null && product.ProductRecipe.Count > 0)
                {
                    foreach (var productRecipe in product.ProductRecipe)
                    {
                        int productRecipeCount = ingredients.Where(x => x.Ingredient_ID == productRecipe.Ingredient_ID).Count();
                        if (productRecipeCount == 0)
                        {
                            return BadRequest(new APIResult
                            {
                                Message = "Ingredient_ID does not exits",
                                Status = 400
                            });
                        }
                    }
                }
                await _productService.AddProductAsync(product);

                result.Data = product;
                result.Message = "Successfully added the product";
                result.Status = 200;

                // Return the created result with the location of the new resource
                return CreatedAtAction(nameof(GetAllProducts), new { id = product.Product_ID }, result);


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
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                await _productService.UpdateProductAsync(product);
                APIResult result = new APIResult
                {
                    Data = await _productService.GetProductByIdAsync(product.Product_ID), // Set the added product as data
                    Message = "Successfully",
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
