using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class drop_OrderProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_RelatedTable_YourTable_YourTableId",
            table: "RelatedTable");

            // Drop indexes if any
            migrationBuilder.DropIndex(
                name: "IX_RelatedTable_YourTableId",
                table: "RelatedTable");

            // Then drop the table
            migrationBuilder.DropTable(
                name: "YourTableName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
