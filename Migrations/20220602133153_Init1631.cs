using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class Init1631 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Messages_LastId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_conversationid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_LastId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "LastId",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "conversationid",
                table: "Messages",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_conversationid",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Conversations",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "ConversationId",
                table: "Messages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "Messages",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ConversationId1",
                table: "Messages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Conversations",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "Contacts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactConversation",
                columns: table => new
                {
                    ContactsId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConversationsId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactConversation", x => new { x.ContactsId, x.ConversationsId });
                    table.ForeignKey(
                        name: "FK_ContactConversation_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactConversation_Conversations_ConversationsId",
                        column: x => x.ConversationsId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ContactId",
                table: "Messages",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId1",
                table: "Messages",
                column: "ConversationId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactConversation_ConversationsId",
                table: "ContactConversation",
                column: "ConversationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Contacts_ContactId",
                table: "Messages",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId1",
                table: "Messages",
                column: "ConversationId1",
                principalTable: "Conversations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Contacts_ContactId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "ContactConversation");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ContactId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Messages",
                newName: "conversationid");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                newName: "IX_Messages_conversationid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Conversations",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "conversationid",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
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

            migrationBuilder.AddColumn<string>(
                name: "LastId",
                table: "Conversations",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Contacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_LastId",
                table: "Conversations",
                column: "LastId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Messages_LastId",
                table: "Conversations",
                column: "LastId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_conversationid",
                table: "Messages",
                column: "conversationid",
                principalTable: "Conversations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
