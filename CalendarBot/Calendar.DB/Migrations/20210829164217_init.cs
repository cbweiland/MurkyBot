using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Calendar.DB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelConfigs",
                columns: table => new
                {
                    ChannelConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ChannelId = table.Column<string>(type: "text", nullable: true),
                    ChannelType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelConfigs", x => x.ChannelConfigId);
                });

            migrationBuilder.CreateTable(
                name: "EventMeetings",
                columns: table => new
                {
                    EventMeetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<string>(type: "text", nullable: true),
                    CheckIn = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    DiscordMessageId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMeetings", x => x.EventMeetingId);
                });

            migrationBuilder.CreateTable(
                name: "SignUp",
                columns: table => new
                {
                    SignUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PersonDiscordId = table.Column<string>(type: "text", nullable: true),
                    EmoteClicked = table.Column<string>(type: "text", nullable: true),
                    DateTimeSignedUp = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventMeetingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUp", x => x.SignUpId);
                    table.ForeignKey(
                        name: "FK_SignUp_EventMeetings_EventMeetingId",
                        column: x => x.EventMeetingId,
                        principalTable: "EventMeetings",
                        principalColumn: "EventMeetingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignUp_EventMeetingId",
                table: "SignUp",
                column: "EventMeetingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelConfigs");

            migrationBuilder.DropTable(
                name: "SignUp");

            migrationBuilder.DropTable(
                name: "EventMeetings");
        }
    }
}
