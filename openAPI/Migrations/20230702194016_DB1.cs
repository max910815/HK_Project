using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace openAPI.Migrations
{
    /// <inheritdoc />
    public partial class DB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    AIFileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AIFileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AIFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIFiles", x => x.AIFileId);
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
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    QA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingVectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AIFileId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embedding", x => x.EmbeddingId);
                    table.ForeignKey(
                        name: "FK_Embedding_AIFiles_AIFileId",
                        column: x => x.AIFileId,
                        principalTable: "AIFiles",
                        principalColumn: "AIFileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    QAHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QAHistoryQ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QAHistoryA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QAHistoryVectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAHistory", x => x.QAHistoryId);
                    table.ForeignKey(
                        name: "FK_QAHistory_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Embedding_AIFileId",
                table: "Embedding",
                column: "AIFileId");

            migrationBuilder.CreateIndex(
                name: "IX_QAHistory_ChatId",
                table: "QAHistory",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicationId",
                table: "Users",
                column: "ApplicationId");
        }

        /// <inheritdoc />
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
