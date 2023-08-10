using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pk.gov.pitb.cmo.directive.domain.Migrations
{
    /// <inheritdoc />
    public partial class directiveeventAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DirectiveEvent_DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "DirectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectiveEvent_RecieverId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectiveEvent_SenderId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectiveEvent_AppUsers_RecieverId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "RecieverId",
                principalSchema: "Auth",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectiveEvent_AppUsers_SenderId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "SenderId",
                principalSchema: "Auth",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectiveEvent_Directives_DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent",
                column: "DirectiveId",
                principalSchema: "eDirectives",
                principalTable: "Directives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectiveEvent_AppUsers_RecieverId",
                schema: "eDirectives",
                table: "DirectiveEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectiveEvent_AppUsers_SenderId",
                schema: "eDirectives",
                table: "DirectiveEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectiveEvent_Directives_DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent");

            migrationBuilder.DropIndex(
                name: "IX_DirectiveEvent_DirectiveId",
                schema: "eDirectives",
                table: "DirectiveEvent");

            migrationBuilder.DropIndex(
                name: "IX_DirectiveEvent_RecieverId",
                schema: "eDirectives",
                table: "DirectiveEvent");

            migrationBuilder.DropIndex(
                name: "IX_DirectiveEvent_SenderId",
                schema: "eDirectives",
                table: "DirectiveEvent");
        }
    }
}
