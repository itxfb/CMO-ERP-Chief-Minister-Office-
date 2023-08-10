using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pk.gov.pitb.cmo.directive.domain.Migrations
{
    /// <inheritdoc />
    public partial class directiveeventAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent");
        }
    }
}
