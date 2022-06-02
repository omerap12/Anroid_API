using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conversationid",
                table: "Messages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Contacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Conversationid",
                table: "Messages",
                column: "Conversationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_Conversationid",
                table: "Messages",
                column: "Conversationid",
                principalTable: "Conversations",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_Conversationid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Conversationid",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Conversationid",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Contacts");
        }
    }
}
