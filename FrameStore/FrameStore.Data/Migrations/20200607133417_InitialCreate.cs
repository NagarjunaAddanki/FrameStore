using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FrameStore.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BrandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionId);
                    table.ForeignKey(
                        name: "FK_Collections_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    StyleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CollectionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.StyleId);
                    table.ForeignKey(
                        name: "FK_Styles_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    FrameId = table.Column<Guid>(nullable: false),
                    FrameColor = table.Column<string>(maxLength: 75, nullable: false),
                    SKU = table.Column<string>(maxLength: 50, nullable: true),
                    Bridge = table.Column<double>(nullable: false),
                    Horizontal = table.Column<double>(nullable: false),
                    Vertical = table.Column<double>(nullable: false),
                    StyleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.FrameId);
                    table.ForeignKey(
                        name: "FK_Frames_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "StyleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameMaterials",
                columns: table => new
                {
                    FrameMaterialId = table.Column<Guid>(nullable: false),
                    FrameId = table.Column<Guid>(nullable: false),
                    MaterialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameMaterials", x => x.FrameMaterialId);
                    table.ForeignKey(
                        name: "FK_FrameMaterials_Frames_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Frames",
                        principalColumn: "FrameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrameMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameTracingRadii",
                columns: table => new
                {
                    FrameTracingRadiusId = table.Column<Guid>(nullable: false),
                    FrameId = table.Column<Guid>(nullable: false),
                    Radius = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameTracingRadii", x => x.FrameTracingRadiusId);
                    table.ForeignKey(
                        name: "FK_FrameTracingRadii_Frames_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Frames",
                        principalColumn: "FrameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_BrandId",
                table: "Collections",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameMaterials_FrameId",
                table: "FrameMaterials",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameMaterials_MaterialId",
                table: "FrameMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_StyleId",
                table: "Frames",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameTracingRadii_FrameId",
                table: "FrameTracingRadii",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_CollectionId",
                table: "Styles",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrameMaterials");

            migrationBuilder.DropTable(
                name: "FrameTracingRadii");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
