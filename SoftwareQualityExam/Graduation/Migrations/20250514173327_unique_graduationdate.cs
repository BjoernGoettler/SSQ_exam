using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.Migrations
{
    /// <inheritdoc />
    public partial class unique_graduationdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GraduationDetails_GraduationDate",
                table: "GraduationDetails",
                column: "GraduationDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GraduationDetails_GraduationDate",
                table: "GraduationDetails");
        }
    }
}
