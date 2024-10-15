using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_API.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyFkToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Benefit
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Benefit",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Benefit_Company_id_company_fk",
                table: "Benefit",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Candidate
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Candidate",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Company_id_company_fk",
                table: "Candidate",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Curriculum
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Curriculum",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculum_Company_id_company_fk",
                table: "Curriculum",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Employee
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Company_id_company_fk",
                table: "Employee",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Filter
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Filter",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Filter_Company_id_company_fk",
                table: "Filter",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Notification
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Company_id_company_fk",
                table: "Notification",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Training
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Training",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Company_id_company_fk",
                table: "Training",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // User
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Company_id_company_fk",
                table: "User",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Vacation
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Vacation",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacation_Company_id_company_fk",
                table: "Vacation",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // Warning
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "Warning",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Warning_Company_id_company_fk",
                table: "Warning",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");

            // WorkSchedule
            migrationBuilder.AddColumn<int>(
                name: "id_company_fk",
                table: "WorkSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedule_Company_id_company_fk",
                table: "WorkSchedule",
                column: "id_company_fk",
                principalTable: "Company",
                principalColumn: "id_company");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Código para eliminar las columnas y claves foráneas si se revierte la migración
            migrationBuilder.DropForeignKey("FK_Benefit_Company_id_company_fk", "Benefit");
            migrationBuilder.DropColumn("id_company_fk", "Benefit");

            migrationBuilder.DropForeignKey("FK_Candidate_Company_id_company_fk", "Candidate");
            migrationBuilder.DropColumn("id_company_fk", "Candidate");

            migrationBuilder.DropForeignKey("FK_Curriculum_Company_id_company_fk", "Curriculum");
            migrationBuilder.DropColumn("id_company_fk", "Curriculum");

            migrationBuilder.DropForeignKey("FK_Employee_Company_id_company_fk", "Employee");
            migrationBuilder.DropColumn("id_company_fk", "Employee");

            migrationBuilder.DropForeignKey("FK_Filter_Company_id_company_fk", "Filter");
            migrationBuilder.DropColumn("id_company_fk", "Filter");

            migrationBuilder.DropForeignKey("FK_Notification_Company_id_company_fk", "Notification");
            migrationBuilder.DropColumn("id_company_fk", "Notification");

            migrationBuilder.DropForeignKey("FK_Training_Company_id_company_fk", "Training");
            migrationBuilder.DropColumn("id_company_fk", "Training");

            migrationBuilder.DropForeignKey("FK_User_Company_id_company_fk", "User");
            migrationBuilder.DropColumn("id_company_fk", "User");

            migrationBuilder.DropForeignKey("FK_Vacation_Company_id_company_fk", "Vacation");
            migrationBuilder.DropColumn("id_company_fk", "Vacation");

            migrationBuilder.DropForeignKey("FK_Warning_Company_id_company_fk", "Warning");
            migrationBuilder.DropColumn("id_company_fk", "Warning");

            migrationBuilder.DropForeignKey("FK_WorkSchedule_Company_id_company_fk", "WorkSchedule");
            migrationBuilder.DropColumn("id_company_fk", "WorkSchedule");
        }
    }
}
