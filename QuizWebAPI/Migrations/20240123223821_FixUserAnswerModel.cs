using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAnswerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answers",
                table: "UserAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answers",
                table: "UserAnswers");
        }
    }
}
