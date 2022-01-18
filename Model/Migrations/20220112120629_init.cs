using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class init : Migration
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
                    Password = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRST_NAME = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LAST_NAME = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PHONE_NUMBER = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BALANCE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IMAGE_CONTENT = table.Column<byte[]>(type: "longblob", nullable: true),
                    IMAGE_TYPE = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_ADMIN = table.Column<ulong>(type: "bit", nullable: false)
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
                    FinalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    BUYER_ID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_AUCTIONS_BT_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "USER_ID");
                    table.ForeignKey(
                        name: "FK_AUCTIONS_BT_USERS_UserId1",
                        column: x => x.UserId1,
                        principalTable: "USERS",
                        principalColumn: "USER_ID");
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUCTION_IMAGES", x => x.IMAGE_ID);
                    table.UniqueConstraint("AK_AUCTION_IMAGES_TempId", x => x.TempId);
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
                    STARTING_PRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Step = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    InstantBuyPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
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
                    MINIMUM_PRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
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
                    PRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
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
                name: "IX_AUCTIONS_BT_UserId",
                table: "AUCTIONS_BT",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUCTIONS_BT_UserId1",
                table: "AUCTIONS_BT",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BIDDING_AUCTION_BIDS_BIDDER_ID",
                table: "BIDDING_AUCTION_BIDS",
                column: "BIDDER_ID");
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
