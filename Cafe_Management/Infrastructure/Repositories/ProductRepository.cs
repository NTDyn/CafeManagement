using Cafe_Management.Code;
using Cafe_Management.Core.Entities;
using Cafe_Management.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Application.Services;
using System.Linq;

namespace Cafe_Management.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IImageRepository _imageRepository;
        private readonly IProductRecipeRepository _productRecipeRepository;

        public ProductRepository(AppDbContext context, IImageRepository imageRepository, IProductRecipeRepository productRecipeRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
            _productRecipeRepository = productRecipeRepository;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            List<ProductRecipe> ProductRecipes = _context.ProductRecipe.ToList();
            return await _context.Products
        .Select(p => new Product
        {

            Product_ID = p.Product_ID,
            Product_Name = p.Product_Name,
            Product_Category = p.Product_Category,
            Price = p.Price,
            Point = p.Point,
            IsActive = p.IsActive,
            CreatedDate = p.CreatedDate,
            ModifiedDate = p.ModifiedDate,
            ProductRecipe = ProductRecipes,
            Product_Image = p.Product_Image
        }).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;

            var maxId = await _context.Products.MaxAsync(p => (int?)p.Product_ID) ?? 0;

            int product_ID = maxId + 1;
            product.IsActive = true;
            if (!string.IsNullOrEmpty(product.Product_Image?.ToString()))
            {
                string saveImageResult = _imageRepository.SaveImage(
                    product.Product_Image.ToString(),
                    product_ID.ToString(),
                    "Products"
                );

                if (saveImageResult.StartsWith("Error"))
                {
                    throw new Exception(saveImageResult);
                }
                product.Product_Image = saveImageResult;
            }
            else
            {
                product.Product_Image = "";
            }

            if (product.ProductRecipe != null && product.ProductRecipe.Count > 0)
            {
                foreach (var recipe in product.ProductRecipe)
                {
                    recipe.Product_ID = product_ID;
                    await _productRecipeRepository.AddProductRecipe(recipe);
                }

            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Product_ID);
            if (existingProduct != null)
            {
                if (product.IsActive != null)
                {
                    existingProduct.IsActive = product.IsActive;
                    existingProduct.ModifiedDate = DateTime.Now;
                }
                if (product.Product_Name != null)
                {
                    existingProduct.Product_Name = product.Product_Name;
                    existingProduct.ModifiedDate = DateTime.Now;
                }
                if (product.Price != null)
                {
                    existingProduct.Price = product.Price;
                    existingProduct.ModifiedDate = DateTime.Now;
                }
                if (product.Product_Category != null)
                {
                    existingProduct.Product_Category = product.Product_Category;
                    existingProduct.ModifiedDate = DateTime.Now;
                }
                if (product.Point != null)
                {
                    existingProduct.Point = product.Point;
                    existingProduct.ModifiedDate = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(product.Product_Image?.ToString()))
                {
                    existingProduct.ModifiedDate = DateTime.Now;
                    string saveImageResult = _imageRepository.SaveImage(
                           product.Product_Image.ToString(),
                           product.Product_ID.ToString(),
                           "Products"
                       );

                    if (saveImageResult.StartsWith("Error"))
                    {
                        throw new Exception(saveImageResult);
                    }
                    existingProduct.Product_Image = saveImageResult;
                }
                if (product.ProductRecipe != null && product.ProductRecipe.Count > 0)
                {
                    var currentProductRecipe = _productRecipeRepository.GetAllRecipeByProductID(product.Product_ID).Result;

                    foreach (var recipe in product.ProductRecipe)
                    {
                        bool exists = currentProductRecipe.Any(r => r.Ingredient_ID == recipe.Ingredient_ID);
                        if (exists == true)
                        {
                            await _productRecipeRepository.UpdateProductRecipe(recipe);
                        }
                        else
                        {
                            await _productRecipeRepository.AddProductRecipe(recipe);
                        }

                    }
                    //DELETE
                    var deleteProductRecipe = product.ProductRecipe.Where(itemA => !currentProductRecipe.Any(itemB => itemB.Ingredient_ID == itemA.Ingredient_ID)).ToList();
                    foreach (var recipe in deleteProductRecipe)
                    {
                        recipe.IsActive = false;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

    }
}

