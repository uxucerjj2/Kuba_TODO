using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kuba_web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Desc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
