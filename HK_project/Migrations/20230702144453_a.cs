using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HK_project.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIFiles",
                columns: table => new
                {
                    AifileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AifileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AifilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIFiles", x => x.AifileId);
                    table.ForeignKey(
                        name: "FK_AIFiles_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Embedding",
                columns: table => new
                {
                    EmbeddingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmbeddingQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbeddingAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbeddingVectors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AifileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embedding", x => x.EmbeddingId);
                    table.ForeignKey(
                        name: "FK_Embedding_AIFiles_AifileId",
                        column: x => x.AifileId,
                        principalTable: "AIFiles",
                        principalColumn: "AifileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QAHistory",
                columns: table => new
                {
                    QahistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QahistoryQ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QahistoryA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QahistoryVectors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAHistory", x => x.QahistoryId);
                    table.ForeignKey(
                        name: "FK_QAHistory_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "MemberId", "MemberEmail", "MemberName", "MemberPassword" },
                values: new object[] { "C0001", "aa@gmail.com", "aa", "aaaaaa" });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "MemberId", "Model", "Parameter" },
                values: new object[] { "A0001", "C0001", null, null });

            migrationBuilder.InsertData(
                table: "AIFiles",
                columns: new[] { "AifileId", "AifilePath", "AifileType", "ApplicationId" },
                values: new object[] { "D0001", "Upload/001.json", "json", "A0001" });

            migrationBuilder.InsertData(
                table: "AIFiles",
                columns: new[] { "AifileId", "AifilePath", "AifileType", "ApplicationId" },
                values: new object[] { "D0002", "Upload/002.json", "json", "A0001" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ApplicationId", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { "U0001", "A0001", "aaa@gmail.com", "aaa", "aaaaaa" });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "ChatId", "ChatName", "ChatTime", "UserId" },
                values: new object[] { "C0001", "Gay", new DateTime(2023, 7, 2, 22, 44, 53, 525, DateTimeKind.Local).AddTicks(1111), "U0001" });

            migrationBuilder.InsertData(
                table: "Embedding",
                columns: new[] { "EmbeddingId", "AifileId", "EmbeddingAnswer", "EmbeddingQuestion", "EmbeddingVectors", "Qa" },
                values: new object[,]
                {
                    { "E0001", "D0001", null, null, "123,345,789", "abc" },
                    { "E0002", "D0001", null, null, "123,345,789", "abc" },
                    { "E0003", "D0002", null, null, "123,345,789", "abc" },
                    { "E0004", "D0002", null, null, "123,345,789", "abc" }
                });

            migrationBuilder.InsertData(
                table: "QAHistory",
                columns: new[] { "QahistoryId", "ChatId", "QahistoryA", "QahistoryQ", "QahistoryVectors" },
                values: new object[] { "Q0001", "C0001", "abc", "abc", "123,456,778" });

            migrationBuilder.InsertData(
                table: "QAHistory",
                columns: new[] { "QahistoryId", "ChatId", "QahistoryA", "QahistoryQ", "QahistoryVectors" },
                values: new object[] { "Q0002", "C0001", "abc", "abc", "123,456,789" });

            migrationBuilder.CreateIndex(
                name: "IX_AIFiles_ApplicationId",
                table: "AIFiles",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_MemberId",
                table: "Applications",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Embedding_AifileId",
                table: "Embedding",
                column: "AifileId");

            migrationBuilder.CreateIndex(
                name: "IX_QAHistory_ChatId",
                table: "QAHistory",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicationId",
                table: "Users",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Embedding");

            migrationBuilder.DropTable(
                name: "QAHistory");

            migrationBuilder.DropTable(
                name: "AIFiles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
