using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.DB.Migrations
{
    public partial class GuildID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuildId",
                table: "ChannelConfigs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "ChannelConfigs");
        }
    }
}
