using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDSExamNet7.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecyclableType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclableType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecyclableItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecyclableTypeId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComputedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclableItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecyclableItem_RecyclableType_RecyclableTypeId",
                        column: x => x.RecyclableTypeId,
                        principalTable: "RecyclableType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecyclableItem_RecyclableTypeId",
                table: "RecyclableItem",
                column: "RecyclableTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecyclableItem");

            migrationBuilder.DropTable(
                name: "RecyclableType");
        }
    }
}
