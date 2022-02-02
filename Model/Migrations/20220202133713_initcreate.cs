using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class initcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    LABEL = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.LABEL);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USERNAME = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EMAIL = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD = table.Column<string>(type: "varchar(256)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRST_NAME = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_NAME = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PHONE_NUMBER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BALANCE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IMAGE_CONTENT = table.Column<byte[]>(type: "longblob", nullable: true),
                    IMAGE_TYPE = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_ADMIN = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AUCTIONS_BT",
                columns: table => new
                {
                    AUCTION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TITLE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRIPTION = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SELLER_ID = table.Column<int>(type: "int", nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FINAL_PRICE = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    BUYER_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUCTIONS_BT", x => x.AUCTION_ID);
                    table.ForeignKey(
                        name: "FK_AUCTIONS_BT_USERS_BUYER_ID",
                        column: x => x.BUYER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUCTIONS_BT_USERS_SELLER_ID",
                        column: x => x.SELLER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USER_HAS_CODES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    USER_ID = table.Column<int>(type: "int", nullable: true),
                    VALUE = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EXPIRES_AT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DISCRIMINATOR = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_HAS_CODES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_HAS_CODES_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AUCTION_HAS_CATEGORIES",
                columns: table => new
                {
                    CATEGORIE = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AUCTION_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUCTION_HAS_CATEGORIES", x => new { x.CATEGORIE, x.AUCTION_ID });
                    table.ForeignKey(
                        name: "FK_AUCTION_HAS_CATEGORIES_AUCTIONS_BT_AUCTION_ID",
                        column: x => x.AUCTION_ID,
                        principalTable: "AUCTIONS_BT",
                        principalColumn: "AUCTION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUCTION_HAS_CATEGORIES_CATEGORIES_CATEGORIE",
                        column: x => x.CATEGORIE,
                        principalTable: "CATEGORIES",
                        principalColumn: "LABEL",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AUCTION_IMAGES",
                columns: table => new
                {
                    IMAGE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AUCTION_ID = table.Column<int>(type: "int", nullable: true),
                    IMAGE_CONTENT = table.Column<byte[]>(type: "longblob", nullable: false),
                    IMAGE_TYPE = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUCTION_IMAGES", x => x.IMAGE_ID);
                    table.ForeignKey(
                        name: "FK_AUCTION_IMAGES_AUCTIONS_BT_AUCTION_ID",
                        column: x => x.AUCTION_ID,
                        principalTable: "AUCTIONS_BT",
                        principalColumn: "AUCTION_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BIDDING_AUCTION",
                columns: table => new
                {
                    AUCTION_ID = table.Column<int>(type: "int", nullable: false),
                    STARTING_PRICE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    STEP = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    INSTANT_BUY_PRICE = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIDDING_AUCTION", x => x.AUCTION_ID);
                    table.ForeignKey(
                        name: "FK_BIDDING_AUCTION_AUCTIONS_BT_AUCTION_ID",
                        column: x => x.AUCTION_ID,
                        principalTable: "AUCTIONS_BT",
                        principalColumn: "AUCTION_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BUY_AUCTION",
                columns: table => new
                {
                    AUCTION_ID = table.Column<int>(type: "int", nullable: false),
                    MINIMUM_PRICE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUY_AUCTION", x => x.AUCTION_ID);
                    table.ForeignKey(
                        name: "FK_BUY_AUCTION_AUCTIONS_BT_AUCTION_ID",
                        column: x => x.AUCTION_ID,
                        principalTable: "AUCTIONS_BT",
                        principalColumn: "AUCTION_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BIDDING_AUCTION_BIDS",
                columns: table => new
                {
                    AUCTION_ID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    BID_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BIDDER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIDDING_AUCTION_BIDS", x => x.AUCTION_ID);
                    table.ForeignKey(
                        name: "FK_BIDDING_AUCTION_BIDS_BIDDING_AUCTION_AUCTION_ID",
                        column: x => x.AUCTION_ID,
                        principalTable: "BIDDING_AUCTION",
                        principalColumn: "AUCTION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BIDDING_AUCTION_BIDS_USERS_BIDDER_ID",
                        column: x => x.BIDDER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AUCTION_HAS_CATEGORIES_AUCTION_ID",
                table: "AUCTION_HAS_CATEGORIES",
                column: "AUCTION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUCTION_IMAGES_AUCTION_ID",
                table: "AUCTION_IMAGES",
                column: "AUCTION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUCTIONS_BT_BUYER_ID",
                table: "AUCTIONS_BT",
                column: "BUYER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUCTIONS_BT_SELLER_ID",
                table: "AUCTIONS_BT",
                column: "SELLER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BIDDING_AUCTION_BIDS_BIDDER_ID",
                table: "BIDDING_AUCTION_BIDS",
                column: "BIDDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_HAS_CODES_USER_ID",
                table: "USER_HAS_CODES",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_USERNAME",
                table: "USERS",
                column: "USERNAME",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUCTION_HAS_CATEGORIES");

            migrationBuilder.DropTable(
                name: "AUCTION_IMAGES");

            migrationBuilder.DropTable(
                name: "BIDDING_AUCTION_BIDS");

            migrationBuilder.DropTable(
                name: "BUY_AUCTION");

            migrationBuilder.DropTable(
                name: "USER_HAS_CODES");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "BIDDING_AUCTION");

            migrationBuilder.DropTable(
                name: "AUCTIONS_BT");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
