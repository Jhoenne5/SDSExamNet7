using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDSExamNet7.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecyclableType_Type",
                table: "RecyclableType",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecyclableType_Type",
                table: "RecyclableType");
        }
    }
}
