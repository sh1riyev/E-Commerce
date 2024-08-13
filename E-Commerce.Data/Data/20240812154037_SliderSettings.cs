using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Data.Data
{
    public partial class SliderSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Check_Addresses_AdressId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_Check_AspNetUsers_UserId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckProduct_Check_CheckId",
                table: "CheckProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slider",
                table: "Slider");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Setting",
                table: "Setting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Check",
                table: "Check");

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
                name: "Slider",
                newName: "Sliders");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Settings");

            migrationBuilder.RenameTable(
                name: "Check",
                newName: "Checks");

            migrationBuilder.RenameIndex(
                name: "IX_Check_UserId",
                table: "Checks",
                newName: "IX_Checks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Check_AdressId",
                table: "Checks",
                newName: "IX_Checks_AdressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 818, DateTimeKind.Utc).AddTicks(1520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 817, DateTimeKind.Utc).AddTicks(8620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 817, DateTimeKind.Utc).AddTicks(6200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(3600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 818, DateTimeKind.Utc).AddTicks(4170),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(6440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 815, DateTimeKind.Utc).AddTicks(4670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(8160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(9750),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(6400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(4780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(2060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 810, DateTimeKind.Utc).AddTicks(310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(5250));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 808, DateTimeKind.Utc).AddTicks(4410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(4480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 805, DateTimeKind.Utc).AddTicks(4990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(6670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(4760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(5520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(2540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(5710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(7160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(2480),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(1510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 19, 40, 36, 685, DateTimeKind.Utc).AddTicks(3670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Utc).AddTicks(6740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 685, DateTimeKind.Utc).AddTicks(1810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 0, 919, DateTimeKind.Utc).AddTicks(5930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Sliders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(5050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Settings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(3470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(2950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Checks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 806, DateTimeKind.Utc).AddTicks(6520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Checks",
                table: "Checks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56e9e4e5-22a8-45a7-ab6c-999180f9d2e2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5772097f-1915-4666-8419-187712648fd8", new DateTime(2024, 8, 12, 19, 40, 36, 685, DateTimeKind.Local).AddTicks(4010), "AQAAAAIAAYagAAAAEE8yzmzLtaDr1qAW19IXzRAFli1ZGms2Awukgp6irrHnLBb/nxqNTf0cz/Zo46pAnw==", "f047feb5-47a3-451c-8ad3-544b78ce9268" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81c5f0b8-be89-4e4a-88ba-01ca7f6244dd",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70cd19cc-d1b9-48e3-8f3d-2db2f1b5b798", new DateTime(2024, 8, 12, 19, 40, 36, 685, DateTimeKind.Local).AddTicks(4080), "AQAAAAIAAYagAAAAEIruCcgd3ApVXSzvK4VtE9hd8omZFT3D319xoi82deZ/eb8eM4AZcUU1+FZxd6Btuw==", "3f442689-ba37-4088-bb6b-002962bc0d4f" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "1fc2368e-1986-4a29-b8ad-039531533ff4", null, false, "Google", null, "www.google.com" },
                    { "2162ada2-a238-49b9-970e-cde704c0c287", null, false, "Facebook", null, "www.facebook.com" },
                    { "5130dc61-8741-4f10-9f63-cdbed38313c2", null, false, "Phone", null, "(+0) 900 901 904" },
                    { "561d5991-2566-4743-9863-67446f0605d3", null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "65f36a55-44af-45bb-8207-e5af335f2e6b", null, false, "Instagram", null, "www.instagram.com" },
                    { "68c179ae-3287-48c3-a6e2-d296d1a4579d", null, false, "LinkedIn", null, "linkedin.com" },
                    { "8e05ea2f-be38-4f1b-b0fb-e1d2509aaf35", null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "9693f0ff-68ed-4699-9869-76881b398d44", null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "abd3e995-5346-4c67-b01b-8a2d37ac88d1", null, false, "Youtube", null, "www.youtube.com" },
                    { "b9eb6205-833f-49b7-ae52-4029e23e1c4b", null, false, "Email", null, "isiriyev@gmail.com" },
                    { "e56e4655-5d36-4e2b-8db0-a7330c0b2505", null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CheckProduct_Checks_CheckId",
                table: "CheckProduct",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Addresses_AdressId",
                table: "Checks",
                column: "AdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_AspNetUsers_UserId",
                table: "Checks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckProduct_Checks_CheckId",
                table: "CheckProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Addresses_AdressId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_Checks_AspNetUsers_UserId",
                table: "Checks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Checks",
                table: "Checks");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "1fc2368e-1986-4a29-b8ad-039531533ff4");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "2162ada2-a238-49b9-970e-cde704c0c287");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "5130dc61-8741-4f10-9f63-cdbed38313c2");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "561d5991-2566-4743-9863-67446f0605d3");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "65f36a55-44af-45bb-8207-e5af335f2e6b");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "68c179ae-3287-48c3-a6e2-d296d1a4579d");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "8e05ea2f-be38-4f1b-b0fb-e1d2509aaf35");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "9693f0ff-68ed-4699-9869-76881b398d44");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "abd3e995-5346-4c67-b01b-8a2d37ac88d1");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "b9eb6205-833f-49b7-ae52-4029e23e1c4b");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "e56e4655-5d36-4e2b-8db0-a7330c0b2505");

            migrationBuilder.RenameTable(
                name: "Sliders",
                newName: "Slider");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Setting");

            migrationBuilder.RenameTable(
                name: "Checks",
                newName: "Check");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_UserId",
                table: "Check",
                newName: "IX_Check_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Checks_AdressId",
                table: "Check",
                newName: "IX_Check_AdressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(5800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 818, DateTimeKind.Utc).AddTicks(1520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(4270),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 817, DateTimeKind.Utc).AddTicks(8620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(3600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 817, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 6, DateTimeKind.Utc).AddTicks(6440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 818, DateTimeKind.Utc).AddTicks(4170));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(7580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 815, DateTimeKind.Utc).AddTicks(4670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(8160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(6400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(4780));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3870),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(2060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(5250),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 810, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CheckProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(4480),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 808, DateTimeKind.Utc).AddTicks(4410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 805, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(6140),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 4, DateTimeKind.Utc).AddTicks(3050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 814, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(5520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(4760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4780),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 798, DateTimeKind.Utc).AddTicks(2540));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BlogComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(3150),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(5710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(4060),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 1, DateTimeKind.Utc).AddTicks(1510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 797, DateTimeKind.Utc).AddTicks(2480));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 10, 22, 9, 0, 919, DateTimeKind.Utc).AddTicks(6740),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 19, 40, 36, 685, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 0, 919, DateTimeKind.Utc).AddTicks(5930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 685, DateTimeKind.Utc).AddTicks(1810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(9160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(5050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Setting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 5, DateTimeKind.Utc).AddTicks(2950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 816, DateTimeKind.Utc).AddTicks(3470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Check",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 2, 9, 1, 3, DateTimeKind.Utc).AddTicks(2990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 23, 40, 36, 806, DateTimeKind.Utc).AddTicks(6520));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slider",
                table: "Slider",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setting",
                table: "Setting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Check",
                table: "Check",
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
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Key", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { "00a39d26-e93f-41d2-b729-0401c1e4a5bd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Email", null, "isiriyev@gmail.com" },
                    { "01291236-58cb-46c6-9c69-baf04cd4ded2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "LinkedIn", null, "linkedin.com" },
                    { "1204505c-6df4-48d4-94af-4a79667ac668", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Shop with Confidence", null, "Our Protection covers your purchase from click to delivery" },
                    { "1290de9a-a618-4725-82ee-14c802cbb46e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Facebook", null, "www.facebook.com" },
                    { "1895cff7-48d3-4823-8bf2-47f6e443c152", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "24/7 Help Center", null, "Round-the-clock assistance for a smooth shopping experience" },
                    { "4a6d74ce-b722-49e8-892d-43d00b73f118", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Location", null, "Neftchi Gurban 168, Baku 1001" },
                    { "a564cd6c-1151-4041-b8b6-847f13653359", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Phone", null, "(+0) 900 901 904" },
                    { "a963906c-ab88-4d6b-813f-53a3d3459baf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Google", null, "www.google.com" },
                    { "b8653442-ef6d-43de-9fe7-370556b2bae4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Youtube", null, "www.youtube.com" },
                    { "e97386a5-8f6f-4103-bf80-6fdee7e38307", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Free Shipping", null, "Free shipping on all US order or order above $100" },
                    { "ee47740a-b138-4418-a853-46f5045bbc63", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Instagram", null, "www.instagram.com" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Addresses_AdressId",
                table: "Check",
                column: "AdressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Check_AspNetUsers_UserId",
                table: "Check",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckProduct_Check_CheckId",
                table: "CheckProduct",
                column: "CheckId",
                principalTable: "Check",
                principalColumn: "Id");
        }
    }
}
