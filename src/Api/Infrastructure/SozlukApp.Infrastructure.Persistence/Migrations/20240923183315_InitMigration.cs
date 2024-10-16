﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SozlukApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EmailConfirmation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryComment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryComment_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryComment_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryFavorite_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryFavorite_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryVote_Entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorite_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorite_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentVote_EntryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "EntryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_CreatedById",
                schema: "dbo",
                table: "Entry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_CreatedById",
                schema: "dbo",
                table: "EntryComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComment_EntryId",
                schema: "dbo",
                table: "EntryComment",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_CreatedById",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorite_EntryCommentId",
                schema: "dbo",
                table: "EntryCommentFavorite",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentVote_EntryCommentId",
                schema: "dbo",
                table: "EntryCommentVote",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_CreatedById",
                schema: "dbo",
                table: "EntryFavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryFavorite_EntryId",
                schema: "dbo",
                table: "EntryFavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryVote_EntryId",
                schema: "dbo",
                table: "EntryVote",
                column: "EntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailConfirmation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryCommentFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryCommentVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryComment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Entry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
