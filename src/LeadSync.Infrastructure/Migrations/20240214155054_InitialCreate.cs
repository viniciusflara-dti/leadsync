using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeadStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { new Guid("51d6382c-63dc-48f1-af99-31151071e9ff"), "stevejobs@apple.com", "Steve", "Jobs", "1234567890" },
                    { new Guid("5cd0ab5c-043d-4645-be77-7353345e6b9c"), "billgates@gmail.com", "Bill", "Gates", "1234567890" },
                    { new Guid("cdbaa142-ff4d-4999-8249-1b5f7dbcc9cf"), "elonmusk@spacex.com", "Elon", "Musk", "1234567890" },
                    { new Guid("e3370d25-d4f0-473a-8b6a-e7a3f30231cf"), "jeffbezos@amazon.com", "Jeff", "Bezos", "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Category", "ContactId", "DateCreated", "Description", "FinalPrice", "LeadStatus", "Price", "Suburb" },
                values: new object[,]
                {
                    { new Guid("77a35649-45a7-40e4-8dbd-43af9e4adea7"), "Interior Painters", new Guid("51d6382c-63dc-48f1-af99-31151071e9ff"), new DateTime(2024, 2, 14, 15, 50, 54, 233, DateTimeKind.Utc).AddTicks(2563), "Internal walls 3 colors", 0m, "New", 600m, "Woolooware 2230" },
                    { new Guid("81efa86c-e990-4f45-8b58-696ac25e041c"), "Home Renovations", new Guid("e3370d25-d4f0-473a-8b6a-e7a3f30231cf"), new DateTime(2024, 2, 14, 15, 50, 54, 233, DateTimeKind.Utc).AddTicks(2566), "There is a two story building at the front of the main house that's about 10x5 that would like to convert into self contained living area.", 0m, "New", 300m, "Quinns Rocks 6030" },
                    { new Guid("b8d2c69c-8794-4efb-8ab1-b82176b331b6"), "Painters", new Guid("5cd0ab5c-043d-4645-be77-7353345e6b9c"), new DateTime(2024, 2, 14, 15, 50, 54, 233, DateTimeKind.Utc).AddTicks(2521), "Need to paint 2 aluminum windows and a sliding glass door", 0m, "New", 62m, "Yanderra 2574" },
                    { new Guid("db5c97d8-5713-47f0-a504-b24e8c1df952"), "General Building Work", new Guid("cdbaa142-ff4d-4999-8249-1b5f7dbcc9cf"), new DateTime(2024, 2, 14, 15, 50, 54, 233, DateTimeKind.Utc).AddTicks(2564), "Plaster exposed brick walls (see photos), square off 2 archways (see photos), and expand pantry (see photos).", 0m, "New", 200m, "Carramar 6031" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leads_ContactId",
                table: "Leads",
                column: "ContactId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
