using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week38MiniProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceUSD = table.Column<double>(type: "float", nullable: false),
                    PriceLocal = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Office = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    assetType = table.Column<int>(type: "int", nullable: true),
                    Mobile_assetType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
