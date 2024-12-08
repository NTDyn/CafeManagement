﻿// <auto-generated />
using System;
using Cafe_Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafe_Management.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cafe_Management.Core.Entities.BatchRecipe", b =>
                {
                    b.Property<int>("BatchRecipe_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatchRecipe_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IngredientResult_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Quality")
                        .HasColumnType("float");

                    b.Property<int>("Staff_ID")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("BatchRecipe_ID");

                    b.ToTable("BatchRecipe", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.BatchRecipeDetail", b =>
                {
                    b.Property<int>("Detail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Detail_ID"), 1L, 1);

                    b.Property<int>("BatchRecipe_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Quality")
                        .HasColumnType("float");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Detail_ID");

                    b.ToTable("BatchRecipeDetail", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Cuppon", b =>
                {
                    b.Property<int>("Cuppon_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cuppon_ID"), 1L, 1);

                    b.Property<int>("ApplyLevel_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cuppon_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cuppon_Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<double>("Disscount")
                        .HasColumnType("float");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Cuppon_ID");

                    b.ToTable("Cuppons", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Customer", b =>
                {
                    b.Property<int>("Customer_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Customer_Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Customer_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Level_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Customer_Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.CustomerLevel", b =>
                {
                    b.Property<int>("Level_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Level_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Level_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PointApply")
                        .HasColumnType("int");

                    b.HasKey("Level_ID");

                    b.ToTable("CustomerLevels", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.HistoryDisscount", b =>
                {
                    b.Property<int>("History_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("History_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cuppon_ID")
                        .HasColumnType("int");

                    b.Property<int>("Customer_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceDisscount")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptDetail_ID")
                        .HasColumnType("int");

                    b.HasKey("History_ID");

                    b.ToTable("HistoryDisscount", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Ingredient", b =>
                {
                    b.Property<int?>("Ingredient_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Ingredient_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Ingredient_Category")
                        .HasColumnType("int");

                    b.Property<string>("Ingredient_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Ingredient_Type")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double?>("MaxPerTransfer")
                        .HasColumnType("float");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("TransferPerMin")
                        .HasColumnType("float");

                    b.Property<string>("Unit_Max")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit_Min")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit_Transfer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Ingredient_ID");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.IngredientCategory", b =>
                {
                    b.Property<int?>("Ingredient_Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Ingredient_Category_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ingredient_Category_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Ingredient_Category_ID");

                    b.ToTable("Ingredient_Categories", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.IngredientSupplierDetail", b =>
                {
                    b.Property<int>("Detail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Detail_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Header_ID")
                        .HasColumnType("int");

                    b.Property<int>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("Quality")
                        .HasColumnType("float");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Detail_ID");

                    b.ToTable("Ingredient_Supplier_Detail", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.IngredientSupplierLink", b =>
                {
                    b.Property<int>("Link_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Link_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StaffApproved_ID")
                        .HasColumnType("int");

                    b.Property<int>("StaffReceived_ID")
                        .HasColumnType("int");

                    b.Property<int>("StaffRequest_ID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Supplier_ID")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("Warehouse_ID")
                        .HasColumnType("int");

                    b.HasKey("Link_ID");

                    b.ToTable("Ingredient_Supplier_Link", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Menu", b =>
                {
                    b.Property<int?>("Menu_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Menu_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Menu_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Menu_ID");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.MenuDetail", b =>
                {
                    b.Property<int?>("Setup_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Setup_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("Menu_ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Product_ID")
                        .HasColumnType("int");

                    b.HasKey("Setup_ID");

                    b.ToTable("MenuDetail", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Permission", b =>
                {
                    b.Property<int>("Permission_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Permission_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Permission_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Permission_ID");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Product", b =>
                {
                    b.Property<int>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Point")
                        .HasColumnType("int");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Product_Category")
                        .HasColumnType("int");

                    b.Property<string>("Product_Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Product_ID");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.ProductCategory", b =>
                {
                    b.Property<int?>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Category_ID"), 1L, 1);

                    b.Property<string>("Category_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Category_ID");

                    b.ToTable("ProductCategory", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.ProductRecipe", b =>
                {
                    b.Property<int?>("Recipe_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Recipe_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Product_ID")
                        .HasColumnType("int");

                    b.Property<double?>("Quantity")
                        .HasColumnType("float");

                    b.Property<int?>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Recipe_ID");

                    b.ToTable("ProductRecipe", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Receipt", b =>
                {
                    b.Property<int>("Receipt_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Receipt_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Staff_ID")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("Receipt_ID");

                    b.ToTable("Receipt", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.ReceiptDetail", b =>
                {
                    b.Property<int>("Detail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Detail_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Receipt_ID")
                        .HasColumnType("int");

                    b.HasKey("Detail_ID");

                    b.ToTable("ReceiptDetail", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.RecipeRaw", b =>
                {
                    b.Property<int>("Recipe_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Recipe_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ingredient_Raw")
                        .HasColumnType("int");

                    b.Property<int>("Ingredient_Result")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("Recipe_ID");

                    b.ToTable("RecipeRaw", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.SpoiledIngredient", b =>
                {
                    b.Property<int>("Spoiled_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Spoiled_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Staff_ID")
                        .HasColumnType("int");

                    b.HasKey("Spoiled_ID");

                    b.ToTable("SpoiledIngredients", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.SpoiledIngredientDetail", b =>
                {
                    b.Property<int>("SpoildDetail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpoildDetail_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Quality")
                        .HasColumnType("float");

                    b.Property<int>("Spoiled_ID")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("SpoildDetail_ID");

                    b.ToTable("SpoiledIngredientDetails", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Staff", b =>
                {
                    b.Property<int>("Staff_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Staff_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffGroup_ID")
                        .HasColumnType("int");

                    b.Property<string>("Staff_FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staff_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Staff_ID");

                    b.ToTable("Staffs", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.StaffGroup", b =>
                {
                    b.Property<int>("StaffGroup_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffGroup_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StaffGroup_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffGroup_ID");

                    b.ToTable("StaffGroups", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.StaffGroupLinkPermission", b =>
                {
                    b.Property<int>("Link_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Link_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Permission_ID")
                        .HasColumnType("int");

                    b.Property<int>("StaffGroup")
                        .HasColumnType("int");

                    b.HasKey("Link_ID");

                    b.ToTable("StaffGroupLinkPermissions", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.StoreIngredient", b =>
                {
                    b.Property<int>("Store_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Store_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("Quality")
                        .HasColumnType("float");

                    b.Property<int>("Warehouse_ID")
                        .HasColumnType("int");

                    b.HasKey("Store_ID");

                    b.ToTable("StoreIngredients", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.StoreProduct", b =>
                {
                    b.Property<int>("StoreProduct_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreProduct_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Warehouse_ID")
                        .HasColumnType("int");

                    b.HasKey("StoreProduct_ID");

                    b.ToTable("StoreProduct", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Supplier", b =>
                {
                    b.Property<int?>("Supplier_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Supplier_ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Supplier_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Supplier_ID");

                    b.ToTable("Suppliers", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.UsedIngredientProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ingredient_ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Receipt_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("UsedIngredientProduct", (string)null);
                });

            modelBuilder.Entity("Cafe_Management.Core.Entities.Warehouse", b =>
                {
                    b.Property<int>("WareHouse_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WareHouse_ID"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WareHouse_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WareHouse_ID");

                    b.ToTable("WareHouse", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
