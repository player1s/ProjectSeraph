using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tier3.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<string>(nullable: true),
                    ProposalNum = table.Column<string>(nullable: true),
                    isFixedSalary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
