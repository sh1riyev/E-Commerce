using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Data.Data
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Product_ProductId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckProduct_Product_ProductId",
                table: "CheckProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_SellerId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_Product_ProductId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Product_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Product_ProductId",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "154611ce-4101-4fd1-a7e6-32e76cdcdcf9");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "349664c0-689a-42da-8929-a7e05e17ffbe");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "3ed3de28-8be8-4e7b-a349-598521e75fa7");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "69d9f6c9-d060-4196-9a33-fbd2acf167b2");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "88ccdfd9-3498-4cef-82a6-00f226971782");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "8e0d1a2b-8d7e-44e0-a00e-cda94f71b7f5");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "acf56bf4-21e3-438e-9fd9-e24829907e34");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "ca1cebbd-34db-4db4-b42f-eaa3176db520");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "ea99906b-fdbe-484f-bab6-1cb6534068ec");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "eca4368e-d100-49a1-ac28-cb928fc8510e");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "fbb80157-f887-4419-af60-cccf9fa4d097");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SellerId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(3290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(9190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(1510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(7440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(6650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 122, DateTimeKind.Utc).AddTicks(5980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(1670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Setting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(9480),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 902, DateTimeKind.Utc).AddTicks(5180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(4050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 904, DateTimeKind.Utc).AddTicks(10));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(4100),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 902, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(2070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(8030));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Country",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(9730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contact",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(8970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "City",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(4060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(9680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(3140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Check",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(7070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 118, DateTimeKind.Utc).AddTicks(9710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(5190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(2370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 900, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(6320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(8320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(3610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(9330),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Basket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(6370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(1550));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Utc).AddTicks(7880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 18, 48, 33, 800, DateTimeKind.Utc).AddTicks(6290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 9, DateTimeKind.Utc).AddTicks(6610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 800, DateTimeKind.Utc).AddTicks(5170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(3340),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(9560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(7230));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "395cfde0-d268-4c30-9fbc-8c74ef0dc39b", new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Local).AddTicks(9260), "AQAAAAIAAYagAAAAEAYdaO0B0ZX4ZVfSkOXAVymIWhcQ2CsS0s0mo6qhhrh5LfRnu4QObNOaVaHFaQ17wQ==", "cf966605-c6ec-4262-ba4f-008a36d5d1a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdb8bf60-0fd0-4c0f-9698-3f9abe2588ff", new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Local).AddTicks(9370), "AQAAAAIAAYagAAAAEAds6bEjTBHDcD3plHmwB6ERbIw5KPK3e3947FG2f/KMQeStHWorCISokaK8IMSg2A==", "23f9363f-6adf-4d64-b0bd-f559266dafe7" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "0e7e4d2c-6c47-4968-b1c3-cc95e99777b1", null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "424d72c8-63d6-4455-ae8a-7effab574367", null, false, "Facebook", null, "www.facebook.com" },
                    { "44704c3b-a982-4a76-a1e3-d1a48cdf166f", null, false, "Google", null, "www.google.com" },
                    { "830145e6-4f97-43c3-b3fe-4bf826a3c604", null, false, "Email", null, "isiriyev@gmail.com" },
                    { "989db7a4-ce3d-4a44-b42e-188f928d6c98", null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" },
                    { "98ea52ab-554d-4ffa-9af4-5e9f980b552d", null, false, "Phone", null, "(+0) 900 901 904" },
                    { "98ff4e50-cb47-4dd6-8d77-c2b73b30fc6d", null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "a5cf7649-13e0-4ecc-b63b-d8d9de330cb4", null, false, "LinkedIn", null, "linkedin.com" },
                    { "b32bfc73-9cf6-41a7-8f14-533aef8c6dcd", null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "deddc006-4025-4a8b-94e1-68beba2dc749", null, false, "Youtube", null, "www.youtube.com" },
                    { "e67bd588-faff-45dd-8534-888308d38161", null, false, "Instagram", null, "www.instagram.com" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckProduct_Products_ProductId",
                table: "CheckProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_Products_ProductId",
                table: "ProductComment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Products_ProductId",
                table: "Wishlist",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckProduct_Products_ProductId",
                table: "CheckProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_Products_ProductId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Products_ProductId",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "0e7e4d2c-6c47-4968-b1c3-cc95e99777b1");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "424d72c8-63d6-4455-ae8a-7effab574367");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "44704c3b-a982-4a76-a1e3-d1a48cdf166f");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "830145e6-4f97-43c3-b3fe-4bf826a3c604");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "989db7a4-ce3d-4a44-b42e-188f928d6c98");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "98ea52ab-554d-4ffa-9af4-5e9f980b552d");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "98ff4e50-cb47-4dd6-8d77-c2b73b30fc6d");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "a5cf7649-13e0-4ecc-b63b-d8d9de330cb4");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "b32bfc73-9cf6-41a7-8f14-533aef8c6dcd");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "deddc006-4025-4a8b-94e1-68beba2dc749");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "e67bd588-faff-45dd-8534-888308d38161");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Product",
                newName: "IX_Product_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "Category",
                newName: "IX_Category_ParentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(9190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(7440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(1510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(6650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 903, DateTimeKind.Utc).AddTicks(1670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 122, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Setting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 902, DateTimeKind.Utc).AddTicks(5180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(9480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 904, DateTimeKind.Utc).AddTicks(10),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 902, DateTimeKind.Utc).AddTicks(310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(8030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(2070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Country",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(5780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contact",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(4990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "City",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(9680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(8750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(3140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Check",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(7070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 899, DateTimeKind.Utc).AddTicks(5190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 118, DateTimeKind.Utc).AddTicks(9710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 900, DateTimeKind.Utc).AddTicks(8300),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(6320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(5450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(3610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(4630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Basket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(1550),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(6370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 18, 48, 33, 800, DateTimeKind.Utc).AddTicks(6290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Utc).AddTicks(7880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 800, DateTimeKind.Utc).AddTicks(5170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 9, DateTimeKind.Utc).AddTicks(6610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 901, DateTimeKind.Utc).AddTicks(9560),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(3340));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 4, 22, 48, 33, 897, DateTimeKind.Utc).AddTicks(7230),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27c9652c-da81-4b2a-b6cc-094d31f9a42a", new DateTime(2024, 8, 4, 18, 48, 33, 800, DateTimeKind.Local).AddTicks(7130), "AQAAAAIAAYagAAAAEBFTyPgc7Rw+hmveE+AJiyCv+ScML+BdSqBC/9uhF3AVzLUdJ4RkvHA0eZ3RR/w6dA==", "6fda35b6-ba8f-4cfa-8729-38d50452ae8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2ab0625-143c-4b3a-9e71-9c192db1e7d4", new DateTime(2024, 8, 4, 18, 48, 33, 800, DateTimeKind.Local).AddTicks(7960), "AQAAAAIAAYagAAAAEEQt/w/VFwEaoxvXnGANZDcTxFV+C0G2dfuem8ShWeFK0cXSB6UqrtbAgb1gNX8xFw==", "ea92ba40-8b1a-44be-8d0d-11f930dc4d37" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "154611ce-4101-4fd1-a7e6-32e76cdcdcf9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "349664c0-689a-42da-8929-a7e05e17ffbe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Google", null, "www.google.com" },
                    { "3ed3de28-8be8-4e7b-a349-598521e75fa7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Youtube", null, "www.youtube.com" },
                    { "69d9f6c9-d060-4196-9a33-fbd2acf167b2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "88ccdfd9-3498-4cef-82a6-00f226971782", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "LinkedIn", null, "linkedin.com" },
                    { "8e0d1a2b-8d7e-44e0-a00e-cda94f71b7f5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" },
                    { "acf56bf4-21e3-438e-9fd9-e24829907e34", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Instagram", null, "www.instagram.com" },
                    { "ca1cebbd-34db-4db4-b42f-eaa3176db520", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "ea99906b-fdbe-484f-bab6-1cb6534068ec", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Facebook", null, "www.facebook.com" },
                    { "eca4368e-d100-49a1-ac28-cb928fc8510e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Phone", null, "(+0) 900 901 904" },
                    { "fbb80157-f887-4419-af60-cccf9fa4d097", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email", null, "isiriyev@gmail.com" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Product_ProductId",
                table: "Basket",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckProduct_Product_ProductId",
                table: "CheckProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_SellerId",
                table: "Product",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_Product_ProductId",
                table: "ProductComment",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Product_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Product_ProductId",
                table: "Wishlist",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
