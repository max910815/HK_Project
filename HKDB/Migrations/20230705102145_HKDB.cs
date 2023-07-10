using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HKDB.Migrations
{
    /// <inheritdoc />
    public partial class HKDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "AiFiles",
                columns: table => new
                {
                    AifileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AifileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AifilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiFiles", x => x.AifileId);
                    table.ForeignKey(
                        name: "FK_AiFiles_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QAHistorys",
                columns: table => new
                {
                    QahistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QahistoryQ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QahistoryA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QahistoryVector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAHistorys", x => x.QahistoryId);
                    table.ForeignKey(
                        name: "FK_QAHistorys_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Embeddings",
                columns: table => new
                {
                    EmbeddingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddingQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbeddingAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmbeddingVector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AifileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embeddings", x => x.EmbeddingId);
                    table.ForeignKey(
                        name: "FK_Embeddings_AiFiles_AifileId",
                        column: x => x.AifileId,
                        principalTable: "AiFiles",
                        principalColumn: "AifileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "MemberEmail", "MemberName", "MemberPassword" },
                values: new object[] { 1, "admin@gmail.com", null, "670b14728ad9902aecba32e22fa4f6bd" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserEmail", "UserName", "UserPassword" },
                values: new object[] { 1, "admin@gmail.com", null, "670b14728ad9902aecba32e22fa4f6bd" });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationName", "MemberId", "Model", "Parameter" },
                values: new object[] { 1, "aaa", 1, null, null });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "ChatId", "ChatName", "ChatTime", "UserId" },
                values: new object[] { 1, null, new DateTime(2023, 7, 5, 18, 21, 45, 356, DateTimeKind.Local).AddTicks(9328), 1 });

            migrationBuilder.InsertData(
                table: "AiFiles",
                columns: new[] { "AifileId", "AifilePath", "AifileType", "ApplicationId", "Language" },
                values: new object[] { 1, "Upload/001.json", "json", 1, null });

            migrationBuilder.InsertData(
                table: "QAHistorys",
                columns: new[] { "QahistoryId", "ChatId", "QahistoryA", "QahistoryQ", "QahistoryVector" },
                values: new object[] { 1, 1, "A", "Q", "123,456,778" });

            migrationBuilder.InsertData(
                table: "Embeddings",
                columns: new[] { "EmbeddingId", "AifileId", "EmbeddingAnswer", "EmbeddingQuestion", "EmbeddingVector", "Qa" },
                values: new object[,]
                {
                    { 1, 1, null, null, "123,345,789", "abc" },
                    { 2, 1, null, null, "123,345,789", "abc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiFiles_ApplicationId",
                table: "AiFiles",
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
                name: "IX_Embeddings_AifileId",
                table: "Embeddings",
                column: "AifileId");

            migrationBuilder.CreateIndex(
                name: "IX_QAHistorys_ChatId",
                table: "QAHistorys",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Embeddings");

            migrationBuilder.DropTable(
                name: "QAHistorys");

            migrationBuilder.DropTable(
                name: "AiFiles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
