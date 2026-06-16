using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DronesPlan.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "planned_assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    assignee = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planned_assignments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "snapshot_batches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snapshot_batches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tracked_issues",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    added_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    manual_priority = table.Column<int>(type: "integer", nullable: true),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracked_issues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "issue_snapshots",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    summary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    issue_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    assignee = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    jira_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    snapshot_batch_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue_snapshots", x => x.id);
                    table.ForeignKey(
                        name: "FK_issue_snapshots_snapshot_batches_snapshot_batch_id",
                        column: x => x.snapshot_batch_id,
                        principalTable: "snapshot_batches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_issue_snapshots_issue_key",
                table: "issue_snapshots",
                column: "issue_key");

            migrationBuilder.CreateIndex(
                name: "IX_issue_snapshots_snapshot_batch_id",
                table: "issue_snapshots",
                column: "snapshot_batch_id");

            migrationBuilder.CreateIndex(
                name: "IX_planned_assignments_assignee",
                table: "planned_assignments",
                column: "assignee");

            migrationBuilder.CreateIndex(
                name: "IX_planned_assignments_date",
                table: "planned_assignments",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_planned_assignments_issue_key_date",
                table: "planned_assignments",
                columns: new[] { "issue_key", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_snapshot_batches_created_at",
                table: "snapshot_batches",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_tracked_issues_is_active",
                table: "tracked_issues",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "IX_tracked_issues_issue_key",
                table: "tracked_issues",
                column: "issue_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue_snapshots");

            migrationBuilder.DropTable(
                name: "planned_assignments");

            migrationBuilder.DropTable(
                name: "tracked_issues");

            migrationBuilder.DropTable(
                name: "snapshot_batches");
        }
    }
}
