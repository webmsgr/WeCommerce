using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddForceChangePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForceChangePassword",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForceChangePassword",
                table: "Users");
        }
    }
}
