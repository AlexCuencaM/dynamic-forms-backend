using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicForms.Migrations
{
    /// <inheritdoc />
    public partial class initial_commit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormTypeData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTypeData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormsAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormsAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormsAnswers_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FormTypeDataId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormInputs_FormTypeData_FormTypeDataId",
                        column: x => x.FormTypeDataId,
                        principalTable: "FormTypeData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormInputs_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FormInputAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FormTypeDataId = table.Column<int>(type: "int", nullable: false),
                    FormAnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInputAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormInputAnswers_FormTypeData_FormTypeDataId",
                        column: x => x.FormTypeDataId,
                        principalTable: "FormTypeData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormInputAnswers_FormsAnswers_FormAnswerId",
                        column: x => x.FormAnswerId,
                        principalTable: "FormsAnswers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormInputAnswers_FormAnswerId",
                table: "FormInputAnswers",
                column: "FormAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputAnswers_FormTypeDataId",
                table: "FormInputAnswers",
                column: "FormTypeDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputs_FormId",
                table: "FormInputs",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputs_FormTypeDataId",
                table: "FormInputs",
                column: "FormTypeDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FormsAnswers_FormId",
                table: "FormsAnswers",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormInputAnswers");

            migrationBuilder.DropTable(
                name: "FormInputs");

            migrationBuilder.DropTable(
                name: "FormsAnswers");

            migrationBuilder.DropTable(
                name: "FormTypeData");

            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
