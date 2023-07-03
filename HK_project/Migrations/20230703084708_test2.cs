using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HK_project.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E0001");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E0002");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E0003");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E0004");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "AIFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AIFiles",
                keyColumn: "AifileId",
                keyValue: "D0001",
                column: "Language",
                value: "en");

            migrationBuilder.UpdateData(
                table: "AIFiles",
                keyColumn: "AifileId",
                keyValue: "D0002",
                column: "Language",
                value: "en");

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: "A0001",
                column: "ApplicationName",
                value: "aaa");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "ChatId",
                keyValue: "C0001",
                column: "ChatTime",
                value: new DateTime(2023, 7, 3, 16, 47, 8, 186, DateTimeKind.Local).AddTicks(5056));

            migrationBuilder.InsertData(
                table: "Embedding",
                columns: new[] { "EmbeddingId", "AifileId", "EmbeddingAnswer", "EmbeddingQuestion", "EmbeddingVectors", "Qa" },
                values: new object[,]
                {
                    { "E00001", "D0001", null, null, "123,345,789", "abc" },
                    { "E00002", "D0001", null, null, "123,345,789", "abc" },
                    { "E00003", "D0002", null, null, "123,345,789", "abc" },
                    { "E00004", "D0002", null, null, "123,345,789", "abc" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E00001");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E00002");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E00003");

            migrationBuilder.DeleteData(
                table: "Embedding",
                keyColumn: "EmbeddingId",
                keyValue: "E00004");

            migrationBuilder.DropColumn(
                name: "ApplicationName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AIFiles");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "ChatId",
                keyValue: "C0001",
                column: "ChatTime",
                value: new DateTime(2023, 7, 2, 22, 44, 53, 525, DateTimeKind.Local).AddTicks(1111));

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
        }
    }
}
