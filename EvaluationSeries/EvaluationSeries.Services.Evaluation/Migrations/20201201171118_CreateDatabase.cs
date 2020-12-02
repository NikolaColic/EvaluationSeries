using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationSeries.Services.Evaluation.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.CriteriaId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: true),
                    Advantage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Favourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.EvaluationId);
                    table.ForeignKey(
                        name: "FK_Evaluation_Series_Id",
                        column: x => x.Id,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluation_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mark",
                columns: table => new
                {
                    MarkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaId = table.Column<int>(type: "int", nullable: true),
                    EvaluationId = table.Column<int>(type: "int", nullable: true),
                    MarkValue = table.Column<int>(type: "int", nullable: false),
                    MarkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mark", x => x.MarkId);
                    table.ForeignKey(
                        name: "FK_Mark_Criteria_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criteria",
                        principalColumn: "CriteriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mark_Evaluation_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluation",
                        principalColumn: "EvaluationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluation_Id",
                table: "Evaluation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_CriteriaId",
                table: "Mark",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mark_EvaluationId",
                table: "Mark",
                column: "EvaluationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mark");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
