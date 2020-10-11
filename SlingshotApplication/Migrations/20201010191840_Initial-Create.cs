using Microsoft.EntityFrameworkCore.Migrations;

namespace SlingshotApplication.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    posX = table.Column<float>(nullable: false),
                    posY = table.Column<float>(nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Edges",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TargetId = table.Column<string>(nullable: true),
                    SourceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edges_Nodes_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Edges_Nodes_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edges_SourceId",
                table: "Edges",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Edges_TargetId",
                table: "Edges",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edges");

            migrationBuilder.DropTable(
                name: "Nodes");
        }
    }
}
