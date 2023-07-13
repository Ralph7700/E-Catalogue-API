using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecatalogbackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryid = table.Column<Guid>(name: "category_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "text", nullable: false),
                    photourl = table.Column<string>(name: "photo_url", type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    subcategoryid = table.Column<Guid>(name: "sub_category_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    FKsubcategorycategoryid = table.Column<Guid>(name: "FK_sub_category_category_id", type: "uuid", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.subcategoryid);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_FK_sub_category_category_id",
                        column: x => x.FKsubcategorycategoryid,
                        principalTable: "Categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: false),
                    FKorderuserid = table.Column<Guid>(name: "FK_order_user_id", type: "uuid", nullable: true),
                    totalprice = table.Column<double>(name: "total_price", type: "double precision", nullable: false),
                    payment = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.orderid);
                    table.ForeignKey(
                        name: "FK_Orders_Users_FK_order_user_id",
                        column: x => x.FKorderuserid,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productid = table.Column<Guid>(name: "product_id", type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    FKproductsubcategoryid = table.Column<Guid>(name: "FK_product_subcategory_id", type: "uuid", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productid);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_FK_product_subcategory_id",
                        column: x => x.FKproductsubcategoryid,
                        principalTable: "SubCategories",
                        principalColumn: "sub_category_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderitemid = table.Column<Guid>(name: "order_item_id", type: "uuid", nullable: false),
                    FKorderitemorderid = table.Column<Guid>(name: "FK_order_item_order_id", type: "uuid", nullable: true),
                    FKorderitemproductid = table.Column<Guid>(name: "FK_order_item_product_id", type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.orderitemid);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_FK_order_item_order_id",
                        column: x => x.FKorderitemorderid,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_FK_order_item_product_id",
                        column: x => x.FKorderitemproductid,
                        principalTable: "Products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    productimageid = table.Column<Guid>(name: "product_image_id", type: "uuid", nullable: false),
                    imageurl = table.Column<string>(name: "image_url", type: "text", nullable: true),
                    FKproductimageproductid = table.Column<Guid>(name: "FK_product_image_product_id", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.productimageid);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_FK_product_image_product_id",
                        column: x => x.FKproductimageproductid,
                        principalTable: "Products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FK_order_item_order_id",
                table: "OrderItems",
                column: "FK_order_item_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FK_order_item_product_id",
                table: "OrderItems",
                column: "FK_order_item_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FK_order_user_id",
                table: "Orders",
                column: "FK_order_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_FK_product_image_product_id",
                table: "ProductImages",
                column: "FK_product_image_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FK_product_subcategory_id",
                table: "Products",
                column: "FK_product_subcategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_FK_sub_category_category_id",
                table: "SubCategories",
                column: "FK_sub_category_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
