using Microsoft.EntityFrameworkCore.Migrations;

namespace PokePredict.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: true),
                    Accuracy = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Target = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Moves_Pokemon_PokemonName",
                        column: x => x.PokemonName,
                        principalTable: "Pokemon",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    PokeName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Effort = table.Column<int>(nullable: false),
                    BaseStat = table.Column<int>(nullable: false),
                    PokemonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => new { x.Name, x.PokeName });
                    table.ForeignKey(
                        name: "FK_Stat_Pokemon_PokemonName",
                        column: x => x.PokemonName,
                        principalTable: "Pokemon",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    PokemonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Type_Pokemon_PokemonName",
                        column: x => x.PokemonName,
                        principalTable: "Pokemon",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatChange",
                columns: table => new
                {
                    MoveName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Change = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatChange", x => new { x.Name, x.MoveName });
                    table.ForeignKey(
                        name: "FK_StatChange_Moves_MoveName",
                        column: x => x.MoveName,
                        principalTable: "Moves",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamageRelation",
                columns: table => new
                {
                    SourceType = table.Column<string>(nullable: false),
                    TargetType = table.Column<string>(nullable: false),
                    DamageMultiplier = table.Column<float>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    TypeName1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageRelation", x => new { x.SourceType, x.TargetType, x.DamageMultiplier });
                    table.ForeignKey(
                        name: "FK_DamageRelation_Type_TypeName",
                        column: x => x.TypeName,
                        principalTable: "Type",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamageRelation_Type_TypeName1",
                        column: x => x.TypeName1,
                        principalTable: "Type",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamageRelation_TypeName",
                table: "DamageRelation",
                column: "TypeName");

            migrationBuilder.CreateIndex(
                name: "IX_DamageRelation_TypeName1",
                table: "DamageRelation",
                column: "TypeName1");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_PokemonName",
                table: "Moves",
                column: "PokemonName");

            migrationBuilder.CreateIndex(
                name: "IX_Stat_PokemonName",
                table: "Stat",
                column: "PokemonName");

            migrationBuilder.CreateIndex(
                name: "IX_StatChange_MoveName",
                table: "StatChange",
                column: "MoveName");

            migrationBuilder.CreateIndex(
                name: "IX_Type_PokemonName",
                table: "Type",
                column: "PokemonName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageRelation");

            migrationBuilder.DropTable(
                name: "Stat");

            migrationBuilder.DropTable(
                name: "StatChange");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Pokemon");
        }
    }
}
