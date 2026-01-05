using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamServer.Migrations
{
    /// <inheritdoc />
    public partial class addSystemUuidProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metadata_SystemUuid",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata_SystemUuid",
                table: "Agents");
        }
    }
}
