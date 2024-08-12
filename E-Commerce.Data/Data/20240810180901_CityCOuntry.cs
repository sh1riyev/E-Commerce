using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Data.Data
{
    public partial class CityCOuntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AspNetUsers_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_City_CityId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Tag_TagId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Check_Address_AdressId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_AspNetUsers_UserId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComment_Products_ProductId",
                table: "ProductComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tag_TagId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_AspNetUsers_UserId",
                table: "Wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Products_ProductId",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComment",
                table: "ProductComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

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
                name: "Wishlist",
                newName: "Wishlists");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "ProductTags");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductComment",
                newName: "ProductComments");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Contact",
                newName: "Contacts");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameTable(
                name: "Basket",
                newName: "Baskets");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_UserId",
                table: "Wishlists",
                newName: "IX_Wishlists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_ProductId",
                table: "Wishlists",
                newName: "IX_Wishlists_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_TagId",
                table: "ProductTags",
                newName: "IX_ProductTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_ProductId",
                table: "ProductTags",
                newName: "IX_ProductTags_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComment_UserId",
                table: "ProductComments",
                newName: "IX_ProductComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComment_ProductId",
                table: "ProductComments",
                newName: "IX_ProductComments_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountryId",
                table: "Cities",
                newName: "IX_Cities_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_UserId",
                table: "Baskets",
                newName: "IX_Baskets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_ProductId",
                table: "Baskets",
                newName: "IX_Baskets_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_UserId",
                table: "Addresses",
                newName: "IX_Addresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CityId",
                table: "Addresses",
                newName: "IX_Addresses_CityId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(3600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(9160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 122, DateTimeKind.Utc).AddTicks(5980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Setting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(2950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(9480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(7580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(3340));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(4480),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(3140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Check",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(2990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 118, DateTimeKind.Utc).AddTicks(9710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(6140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(3150),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Utc).AddTicks(6740),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Utc).AddTicks(7880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(5800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(4270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(1510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(6440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(8160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(6400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(2070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(5250),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(5520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(1510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(6370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 0, 919, DateTimeKind.Utc).AddTicks(5930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 9, DateTimeKind.Utc).AddTicks(6610));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComments",
                table: "ProductComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a850a882-f8ba-43c6-b718-55409241c979", new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Local).AddTicks(7020), "AQAAAAIAAYagAAAAENWOC6YJ2CiBk+DkkFlCMJ0W1Z10rpvYctTzLZdN5mK3S+jhTRTx0U8yFLNZ84n1Ew==", "ea86dc70-fe61-4764-b768-a8e507b5b270" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "296fa02b-d316-4d77-afa0-1cb4a4b16d1d", new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Local).AddTicks(7100), "AQAAAAIAAYagAAAAECkufTtyCq4wzwd+CX8F8fODmcipSdjuLauFdZL8lhmsIwWpgXH/nWXy12C1pWG+XQ==", "05aae721-bca2-4c1e-82a2-feee1161a6c9" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "00a39d26-e93f-41d2-b729-0401c1e4a5bd", null, false, "Email", null, "isiriyev@gmail.com" },
                    { "01291236-58cb-46c6-9c69-baf04cd4ded2", null, false, "LinkedIn", null, "linkedin.com" },
                    { "1204505c-6df4-48d4-94af-4a79667ac668", null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "1290de9a-a618-4725-82ee-14c802cbb46e", null, false, "Facebook", null, "www.facebook.com" },
                    { "1895cff7-48d3-4823-8bf2-47f6e443c152", null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "4a6d74ce-b722-49e8-892d-43d00b73f118", null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" },
                    { "a564cd6c-1151-4041-b8b6-847f13653359", null, false, "Phone", null, "(+0) 900 901 904" },
                    { "a963906c-ab88-4d6b-813f-53a3d3459baf", null, false, "Google", null, "www.google.com" },
                    { "b8653442-ef6d-43de-9fe7-370556b2bae4", null, false, "Youtube", null, "www.youtube.com" },
                    { "e97386a5-8f6f-4103-bf80-6fdee7e38307", null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "ee47740a-b138-4418-a853-46f5045bbc63", null, false, "Instagram", null, "www.instagram.com" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Addresses_AdressId",
                table: "Check",
                column: "AdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_UserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_UserId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Tags_TagId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Check_Addresses_AdressId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Products_ProductId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Products_ProductId",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTags",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComments",
                table: "ProductComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "00a39d26-e93f-41d2-b729-0401c1e4a5bd");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "01291236-58cb-46c6-9c69-baf04cd4ded2");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "1204505c-6df4-48d4-94af-4a79667ac668");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "1290de9a-a618-4725-82ee-14c802cbb46e");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "1895cff7-48d3-4823-8bf2-47f6e443c152");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "4a6d74ce-b722-49e8-892d-43d00b73f118");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "a564cd6c-1151-4041-b8b6-847f13653359");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "a963906c-ab88-4d6b-813f-53a3d3459baf");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "b8653442-ef6d-43de-9fe7-370556b2bae4");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "e97386a5-8f6f-4103-bf80-6fdee7e38307");

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: "ee47740a-b138-4418-a853-46f5045bbc63");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                newName: "Wishlist");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "ProductTags",
                newName: "ProductTag");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductComments",
                newName: "ProductComment");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Contacts",
                newName: "Contact");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "Basket");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlist",
                newName: "IX_Wishlist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_ProductId",
                table: "Wishlist",
                newName: "IX_Wishlist_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTag",
                newName: "IX_ProductTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTag",
                newName: "IX_ProductTag_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComment",
                newName: "IX_ProductComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComment",
                newName: "IX_ProductComment_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_CountryId",
                table: "City",
                newName: "IX_City_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_UserId",
                table: "Basket",
                newName: "IX_Basket_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductId",
                table: "Basket",
                newName: "IX_Basket_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_UserId",
                table: "Address",
                newName: "IX_Address_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CityId",
                table: "Address",
                newName: "IX_Address_CityId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(3600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 122, DateTimeKind.Utc).AddTicks(5980),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Setting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(9480),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(2950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(3340),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(3140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Check",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 118, DateTimeKind.Utc).AddTicks(9710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1910),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(2370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(8320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(9330),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 18, 32, 1, 9, DateTimeKind.Utc).AddTicks(7880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Utc).AddTicks(6740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(3290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(1510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTag",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 123, DateTimeKind.Utc).AddTicks(4050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(6440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(4100),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(8160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 121, DateTimeKind.Utc).AddTicks(2070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(6400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Country",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(9730),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contact",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 120, DateTimeKind.Utc).AddTicks(8970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "City",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 119, DateTimeKind.Utc).AddTicks(4060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(5250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 117, DateTimeKind.Utc).AddTicks(1090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(5520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Basket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 116, DateTimeKind.Utc).AddTicks(6370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(1510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 32, 1, 9, DateTimeKind.Utc).AddTicks(6610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 0, 919, DateTimeKind.Utc).AddTicks(5930));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComment",
                table: "ProductComment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
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
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "0e7e4d2c-6c47-4968-b1c3-cc95e99777b1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "424d72c8-63d6-4455-ae8a-7effab574367", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Facebook", null, "www.facebook.com" },
                    { "44704c3b-a982-4a76-a1e3-d1a48cdf166f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Google", null, "www.google.com" },
                    { "830145e6-4f97-43c3-b3fe-4bf826a3c604", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email", null, "isiriyev@gmail.com" },
                    { "989db7a4-ce3d-4a44-b42e-188f928d6c98", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" },
                    { "98ea52ab-554d-4ffa-9af4-5e9f980b552d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Phone", null, "(+0) 900 901 904" },
                    { "98ff4e50-cb47-4dd6-8d77-c2b73b30fc6d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "a5cf7649-13e0-4ecc-b63b-d8d9de330cb4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "LinkedIn", null, "linkedin.com" },
                    { "b32bfc73-9cf6-41a7-8f14-533aef8c6dcd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "deddc006-4025-4a8b-94e1-68beba2dc749", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Youtube", null, "www.youtube.com" },
                    { "e67bd588-faff-45dd-8534-888308d38161", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Instagram", null, "www.instagram.com" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AspNetUsers_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_City_CityId",
                table: "Address",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_AspNetUsers_UserId",
                table: "Basket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Products_ProductId",
                table: "Basket",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Tag_TagId",
                table: "BlogTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Address_AdressId",
                table: "Check",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComment_AspNetUsers_UserId",
                table: "ProductComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Products_Brand_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brand",
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
                name: "FK_ProductTag_Tag_TagId",
                table: "ProductTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_AspNetUsers_UserId",
                table: "Wishlist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Products_ProductId",
                table: "Wishlist",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
