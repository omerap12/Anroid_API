using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_Conversationid",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Conversationid",
                table: "Messages",
                newName: "conversationid");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_Conversationid",
                table: "Messages",
                newName: "IX_Messages_conversationid");

            migrationBuilder.AlterColumn<int>(
                name: "conversationid",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Conversations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_conversationid",
                table: "Messages",
                column: "conversationid",
                principalTable: "Conversations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_conversationid",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "conversationid",
                table: "Messages",
                newName: "Conversationid");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_conversationid",
                table: "Messages",
                newName: "IX_Messages_Conversationid");

            migrationBuilder.AlterColumn<string>(
                name: "Conversationid",
                table: "Messages",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Conversations",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_Conversationid",
                table: "Messages",
                column: "Conversationid",
                principalTable: "Conversations",
                principalColumn: "id");
        }
    }
}
