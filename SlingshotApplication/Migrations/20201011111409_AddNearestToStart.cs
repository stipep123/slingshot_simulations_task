using Microsoft.EntityFrameworkCore.Migrations;

namespace SlingshotApplication.Migrations
{
    public partial class AddNearestToStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "posY",
                table: "Nodes",
                newName: "PosY");

            migrationBuilder.RenameColumn(
                name: "posX",
                table: "Nodes",
                newName: "PosX");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NearestToStartId",
                table: "Nodes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visited",
                table: "Nodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Edges",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NearestToStartId",
                table: "Nodes",
                column: "NearestToStartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Nodes_NearestToStartId",
                table: "Nodes",
                column: "NearestToStartId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Nodes_NearestToStartId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_NearestToStartId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "NearestToStartId",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Visited",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Edges");

            migrationBuilder.RenameColumn(
                name: "PosY",
                table: "Nodes",
                newName: "posY");

            migrationBuilder.RenameColumn(
                name: "PosX",
                table: "Nodes",
                newName: "posX");
        }
    }
}
