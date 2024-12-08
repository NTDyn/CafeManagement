using Microsoft.EntityFrameworkCore;
using Cafe_Management.Core.Entities;

namespace Cafe_Management.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BatchRecipe> BatchRecipe { get; set; }
        public DbSet<BatchRecipeDetail> BatchRecipeDetail { get; set; }
        public DbSet<Cuppon> Cuppon { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerLevel> CustomerLevel { get; set; }
        public DbSet<HistoryDisscount> HistoryDisscount { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<IngredientCategory> IngredientCategory { get; set; }
        public DbSet<IngredientSupplierDetail> IngredientSupplierDetail { get; set; }
        public DbSet<IngredientSupplierLink> IngredientSupplierLink { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuDetail> MenuDetail { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductRecipe> ProductRecipe { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetail { get; set; }
        public DbSet<RecipeRaw> RecipeRaw { get; set; }
        public DbSet<SpoiledIngredient> SpoiledIngredient { get; set; }
        public DbSet<SpoiledIngredientDetail> SpoiledIngredientDetail { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<StaffGroup> StaffGroup { get; set; }
        public DbSet<StaffGroupLinkPermission> StaffGroupLinkPermission { get; set; }
        public DbSet<StoreIngredient> StoreIngredient { get; set; }
        public DbSet<StoreProduct> StoreProduct { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<UsedIngredientProduct> UsedIngredientProduct { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Product_ID);
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().Ignore(e => e.ProductRecipe);

            modelBuilder.Entity<ProductRecipe>().HasKey(pr => pr.Recipe_ID);
            modelBuilder.Entity<ProductRecipe>().ToTable("ProductRecipe");
            

            modelBuilder.Entity<ProductCategory>().HasKey(pc => pc.Category_ID);
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
           

            modelBuilder.Entity<BatchRecipe>().HasKey(b => b.BatchRecipe_ID);
            modelBuilder.Entity<BatchRecipe>().ToTable("BatchRecipe");
            modelBuilder.Entity<BatchRecipe>().Ignore(b => b.Details);

            modelBuilder.Entity<BatchRecipeDetail>().HasKey(bd => bd.Detail_ID);
            modelBuilder.Entity<BatchRecipeDetail>().ToTable("BatchRecipeDetail");


            modelBuilder.Entity<Cuppon>().HasKey(c => c.Cuppon_ID);
            modelBuilder.Entity<Cuppon>().ToTable("Cuppons");

            modelBuilder.Entity<Customer>().HasKey(cus => cus.Customer_Id);
            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<CustomerLevel>().HasKey(cus => cus.Level_ID);
            modelBuilder.Entity<CustomerLevel>().ToTable("CustomerLevels");

            modelBuilder.Entity<HistoryDisscount>().HasKey(d => d.History_ID);
            modelBuilder.Entity<HistoryDisscount>().ToTable("HistoryDisscount");

            modelBuilder.Entity<Ingredient>().HasKey(d => d.Ingredient_ID);
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");

            modelBuilder.Entity<IngredientCategory>().HasKey(d => d.Ingredient_Category_ID);
            modelBuilder.Entity<IngredientCategory>().ToTable("Ingredient_Categories");

            modelBuilder.Entity<IngredientSupplierDetail>().HasKey(d => d.Detail_ID);
            modelBuilder.Entity<IngredientSupplierDetail>().ToTable("Ingredient_Supplier_Detail");

            modelBuilder.Entity<IngredientSupplierLink>().HasKey(d => d.Link_ID);
            modelBuilder.Entity<IngredientSupplierLink>().ToTable("Ingredient_Supplier_Link");

            modelBuilder.Entity<Menu>().HasKey(m => m.Menu_ID);
            modelBuilder.Entity<Menu>().ToTable("Menu");

            modelBuilder.Entity<MenuDetail>().HasKey(d => d.Setup_ID);
            modelBuilder.Entity<MenuDetail>().ToTable("MenuDetail");
            modelBuilder.Entity<MenuDetail>().Ignore(e => e.Product);

            modelBuilder.Entity<Permission>().HasKey(p => p.Permission_ID);
            modelBuilder.Entity<Permission>().ToTable("Permissions");


            modelBuilder.Entity<Receipt>().HasKey(p => p.Receipt_ID);
            modelBuilder.Entity<Receipt>().ToTable("Receipt");
            modelBuilder.Entity<Receipt>().Ignore(p => p.Details);

            modelBuilder.Entity<ReceiptDetail>().HasKey(p => p.Detail_ID);
            modelBuilder.Entity<ReceiptDetail>().ToTable("ReceiptDetail");

            modelBuilder.Entity<RecipeRaw>().HasKey(p => p.Recipe_ID);
            modelBuilder.Entity<RecipeRaw>().ToTable("RecipeRaw");

            modelBuilder.Entity<SpoiledIngredient>().HasKey(p => p.Spoiled_ID);
            modelBuilder.Entity<SpoiledIngredient>().ToTable("SpoiledIngredients");
            modelBuilder.Entity<SpoiledIngredient>().Ignore(e=>e.Details);

            modelBuilder.Entity<SpoiledIngredientDetail>().HasKey(p => p.SpoildDetail_ID);
            modelBuilder.Entity<SpoiledIngredientDetail>().ToTable("SpoiledIngredientDetails");

            modelBuilder.Entity<Staff>().HasKey(s => s.Staff_ID);
            modelBuilder.Entity<Staff>().ToTable("Staffs");

            modelBuilder.Entity<StaffGroup>().HasKey(g => g.StaffGroup_ID);
            modelBuilder.Entity<StaffGroup>().ToTable("StaffGroups");
            modelBuilder.Entity<StaffGroup>().Ignore(s => s.Permissions);

            modelBuilder.Entity<StaffGroupLinkPermission>().HasKey(g => g.Link_ID);
            modelBuilder.Entity<StaffGroupLinkPermission>().ToTable("StaffGroupLinkPermissions");
           

            modelBuilder.Entity<StoreIngredient>().HasKey(si => si.Store_ID);
            modelBuilder.Entity<StoreIngredient>().ToTable("StoreIngredients");

            modelBuilder.Entity<StoreProduct>().HasKey(sp => sp.StoreProduct_ID);
            modelBuilder.Entity<StoreProduct>().ToTable("StoreProduct");

            modelBuilder.Entity<Supplier>().HasKey(p => p.Supplier_ID);
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");

            modelBuilder.Entity<UsedIngredientProduct>().HasKey(us => us.ID);
            modelBuilder.Entity<UsedIngredientProduct>().ToTable("UsedIngredientProduct");

            modelBuilder.Entity<Warehouse>().HasKey(w => w.WareHouse_ID);
            modelBuilder.Entity<Warehouse>().ToTable("WareHouse");



        }
    }
}
