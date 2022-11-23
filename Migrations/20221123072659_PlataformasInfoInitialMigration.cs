using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlataformasWeb.Migrations
{
    /// <inheritdoc />
    public partial class PlataformasInfoInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    PlatformId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "The reliable one, always creating new content.", "Netflix" },
                    { 2, "The cheapest one, accesible for everyone.", "Amazon Prime Video" },
                    { 3, "The youngest of them all.", "Disney+" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "PlatformId", "Title" },
                values: new object[,]
                {
                    { 1, "Película romántica de época basada en una novela.", 1, "Orgullo y Prejuicio" },
                    { 2, "Película de ciencia ficción sobre la galaxia.", 1, "Star Wars" },
                    { 3, "Película de comedia americana sobre un grupo de amigos.", 2, "Niños grandes" },
                    { 4, "Película musical de animación sobre un teatro de animales.", 2, "Canta" },
                    { 5, "Película musical sobre un grupo de adolescentes que van a un campamento de verano.", 3, "Camp Rock" },
                    { 6, "Película infantil de animación sobre el valor de la amistad y la familia.", 3, "Hermano Oso" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PlatformId",
                table: "Movies",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
