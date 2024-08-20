using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClarityImplementation.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedenrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentAndEligibilityContacts_COBRAPlanId",
                table: "EnrollmentAndEligibilityContacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 17, 18, 20, 310, DateTimeKind.Local).AddTicks(6789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 13, 10, 32, 616, DateTimeKind.Local).AddTicks(5540));

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentAndEligibilityContacts_COBRAPlanId",
                table: "EnrollmentAndEligibilityContacts",
                column: "COBRAPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnrollmentAndEligibilityContacts_COBRAPlanId",
                table: "EnrollmentAndEligibilityContacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 20, 13, 10, 32, 616, DateTimeKind.Local).AddTicks(5540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 20, 17, 18, 20, 310, DateTimeKind.Local).AddTicks(6789));

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentAndEligibilityContacts_COBRAPlanId",
                table: "EnrollmentAndEligibilityContacts",
                column: "COBRAPlanId",
                unique: true);
        }
    }
}
