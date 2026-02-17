using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsStar",
                table: "Messages",
                newName: "IsSenderStarred");

            migrationBuilder.AddColumn<bool>(
                name: "IsReceiverStarred",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReceiverStarred",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "IsSenderStarred",
                table: "Messages",
                newName: "IsStar");
        }
    }
}
