using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_API.Migrations
{
    /// <inheritdoc />
    public partial class ERPDbContextModelSnapshotMigrationNewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benefit",
                columns: table => new
                {
                    id_benefits = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_benefits = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    description_benefits = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefit", x => x.id_benefits);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id_company = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    code_company = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    description_company = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    location_company = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    url_company = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    status_company = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Company__5D0E9F0690327F24", x => x.id_company);
                });

            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    id_filter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_filter = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    description_filter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_filter = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true),
                    observation_about_candidate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Filter__F614A2C5F995FDDE", x => x.id_filter);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    id_permission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_permission = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    description_permission = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permissi__5180B3BF93E2630D", x => x.id_permission);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_role = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Description_role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3D48441D62B42DA8", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    id_training = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_training = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    description_training = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_training", x => x.id_training);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id_person = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    second_last_name_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    age_person = table.Column<double>(type: "float", nullable: true),
                    phone_number_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    address_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    nationality_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    state_person = table.Column<byte>(type: "tinyint", nullable: true),
                    identification_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    email_person = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    id_company_fk = table.Column<int>(type: "int", nullable: true),
                    UUID_person = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Person__E9AB6A419FC3C7D6", x => x.id_person);
                    table.ForeignKey(
                        name: "FK_Person_Company",
                        column: x => x.id_company_fk,
                        principalTable: "Company",
                        principalColumn: "id_company");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    id_role_permission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_role_fk = table.Column<int>(type: "int", nullable: true),
                    id_permission_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RolePerm__B8BA225BE4556F0E", x => x.id_role_permission);
                    table.ForeignKey(
                        name: "FK__RolePermi__id_pe__4AB81AF0",
                        column: x => x.id_permission_fk,
                        principalTable: "Permission",
                        principalColumn: "id_permission");
                    table.ForeignKey(
                        name: "FK__RolePermi__id_ro__49C3F6B7",
                        column: x => x.id_role_fk,
                        principalTable: "Role",
                        principalColumn: "id_role");
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    id_candidate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_person_fk = table.Column<int>(type: "int", nullable: true),
                    application_date_candidate = table.Column<DateTime>(type: "datetime", nullable: true),
                    position_applied_candidate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__3CD1A87F17A41FDF", x => x.id_candidate);
                    table.ForeignKey(
                        name: "FK__Candidate__id_pe__6EF57B66",
                        column: x => x.id_person_fk,
                        principalTable: "Person",
                        principalColumn: "id_person");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_user = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    password_user = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    creation_date_user = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_person_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__D2D14637BD867DE2", x => x.id_user);
                    table.ForeignKey(
                        name: "FK__User__id_person___398D8EEE",
                        column: x => x.id_person_fk,
                        principalTable: "Person",
                        principalColumn: "id_person");
                });

            migrationBuilder.CreateTable(
                name: "CurriculumCandidate",
                columns: table => new
                {
                    id_curriculumCandidate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_candidate_fk = table.Column<int>(type: "int", nullable: true),
                    path_curriculum_candidate = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__1F7DA07626B967EF", x => x.id_curriculumCandidate);
                    table.ForeignKey(
                        name: "FK__Curriculu__id_ca__71D1E811",
                        column: x => x.id_candidate_fk,
                        principalTable: "Candidate",
                        principalColumn: "id_candidate");
                });

            migrationBuilder.CreateTable(
                name: "FilterCandidate",
                columns: table => new
                {
                    id_filter_candidate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_filter_fk = table.Column<int>(type: "int", nullable: true),
                    id_candidate_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FilterCa__3808217E2FEF49C3", x => x.id_filter_candidate);
                    table.ForeignKey(
                        name: "FK__FilterCan__id_ca__778AC167",
                        column: x => x.id_candidate_fk,
                        principalTable: "Candidate",
                        principalColumn: "id_candidate");
                    table.ForeignKey(
                        name: "FK__FilterCan__id_fi__76969D2E",
                        column: x => x.id_filter_fk,
                        principalTable: "Filter",
                        principalColumn: "id_filter");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id_employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hiring_date_employee = table.Column<DateTime>(type: "date", nullable: false),
                    net_salary_employee = table.Column<double>(type: "float", nullable: false),
                    position_employee = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    department_employee = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    vacations_employee = table.Column<int>(type: "int", nullable: true),
                    id_user_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__F807679C158F656B", x => x.id_employee);
                    table.ForeignKey(
                        name: "FK__Employee__id_use__3C69FB99",
                        column: x => x.id_user_fk,
                        principalTable: "User",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    id_session = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token_session = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
                    creation_date_session = table.Column<DateTime>(type: "datetime", nullable: true),
                    expiration_date_session = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_date_session = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_user_fk = table.Column<int>(type: "int", nullable: true),
                    status_session = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Session__A9E494D0F73F88E4", x => x.id_session);
                    table.ForeignKey(
                        name: "FK__Session__id_user__3F466844",
                        column: x => x.id_user_fk,
                        principalTable: "User",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    id_user_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_role_fk = table.Column<int>(type: "int", nullable: true),
                    id_user_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__4FD2ABB33BD08450", x => x.id_user_role);
                    table.ForeignKey(
                        name: "FK__UserRole__id_rol__440B1D61",
                        column: x => x.id_role_fk,
                        principalTable: "Role",
                        principalColumn: "id_role");
                    table.ForeignKey(
                        name: "FK__UserRole__id_use__44FF419A",
                        column: x => x.id_user_fk,
                        principalTable: "User",
                        principalColumn: "id_user");
                });

            migrationBuilder.CreateTable(
                name: "BenefitsEmployee",
                columns: table => new
                {
                    id_benefits_employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_employee_fk = table.Column<int>(type: "int", nullable: false),
                    id_benefits_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitsEmployee", x => x.id_benefits_employee);
                    table.ForeignKey(
                        name: "FK_BenefitsEmployee_Benefit",
                        column: x => x.id_benefits_fk,
                        principalTable: "Benefit",
                        principalColumn: "id_benefits");
                    table.ForeignKey(
                        name: "FK_BenefitsEmployee_Employee",
                        column: x => x.id_employee_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    id_curriculum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_employee_fk = table.Column<int>(type: "int", nullable: true),
                    path_file_curriculum = table.Column<string>(type: "nchar(1000)", fixedLength: true, maxLength: 1000, nullable: true),
                    date_uploaded = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__8151415FCF29CF7D", x => x.id_curriculum);
                    table.ForeignKey(
                        name: "FK__Curriculu__id_em__4D94879B",
                        column: x => x.id_employee_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    id_notification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_name_notification = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    description_notification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_notification = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_employeed_fk = table.Column<int>(type: "int", nullable: false),
                    status_notification = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id_notification);
                    table.ForeignKey(
                        name: "FK_Notification_Employee",
                        column: x => x.id_employeed_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateTable(
                name: "TrainingEmployee",
                columns: table => new
                {
                    id_training_employee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_training_fk = table.Column<int>(type: "int", nullable: false),
                    id_employee_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEmployee", x => x.id_training_employee);
                    table.ForeignKey(
                        name: "FK_TrainingEmployee_Employee",
                        column: x => x.id_employee_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                    table.ForeignKey(
                        name: "FK_TrainingEmployee_training",
                        column: x => x.id_training_fk,
                        principalTable: "Training",
                        principalColumn: "id_training");
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new
                {
                    id_vacations = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_date = table.Column<DateTime>(type: "date", nullable: true),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    approved = table.Column<byte>(type: "tinyint", nullable: true),
                    days_taken = table.Column<int>(type: "int", nullable: true),
                    id_employee_fk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vacation__57B18186994EE17E", x => x.id_vacations);
                    table.ForeignKey(
                        name: "FK__Vacations__id_em__5070F446",
                        column: x => x.id_employee_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateTable(
                name: "Warning",
                columns: table => new
                {
                    id_warning = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reason_warning = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    descrption_warning = table.Column<string>(type: "nchar(2000)", fixedLength: true, maxLength: 2000, nullable: true),
                    id_employeed_fk = table.Column<int>(type: "int", nullable: false),
                    date_warning = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warning", x => x.id_warning);
                    table.ForeignKey(
                        name: "FK_Warning_Employee",
                        column: x => x.id_employeed_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedule",
                columns: table => new
                {
                    id_work_schedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_work_schedule = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    start_work_schedule = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_work_schedule = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_employeer_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workSchedule", x => x.id_work_schedule);
                    table.ForeignKey(
                        name: "FK_workSchedule_Employee",
                        column: x => x.id_employeer_fk,
                        principalTable: "Employee",
                        principalColumn: "id_employee");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenefitsEmployee_id_benefits_fk",
                table: "BenefitsEmployee",
                column: "id_benefits_fk");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitsEmployee_id_employee_fk",
                table: "BenefitsEmployee",
                column: "id_employee_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_id_person_fk",
                table: "Candidate",
                column: "id_person_fk");

            migrationBuilder.CreateIndex(
                name: "UQ__Company__A8E62A4EB7995DF6",
                table: "Company",
                column: "code_company",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_id_employee_fk",
                table: "Curriculum",
                column: "id_employee_fk");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumCandidate_id_candidate_fk",
                table: "CurriculumCandidate",
                column: "id_candidate_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_id_user_fk",
                table: "Employee",
                column: "id_user_fk");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCandidate_id_candidate_fk",
                table: "FilterCandidate",
                column: "id_candidate_fk");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCandidate_id_filter_fk",
                table: "FilterCandidate",
                column: "id_filter_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_id_employeed_fk",
                table: "Notification",
                column: "id_employeed_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Person_id_company_fk",
                table: "Person",
                column: "id_company_fk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_id_permission_fk",
                table: "RolePermission",
                column: "id_permission_fk");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_id_role_fk",
                table: "RolePermission",
                column: "id_role_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Session_id_user_fk",
                table: "Session",
                column: "id_user_fk");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEmployee_id_employee_fk",
                table: "TrainingEmployee",
                column: "id_employee_fk");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEmployee_id_training_fk",
                table: "TrainingEmployee",
                column: "id_training_fk");

            migrationBuilder.CreateIndex(
                name: "IX_User_id_person_fk",
                table: "User",
                column: "id_person_fk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_id_role_fk",
                table: "UserRole",
                column: "id_role_fk");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_id_user_fk",
                table: "UserRole",
                column: "id_user_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_id_employee_fk",
                table: "Vacation",
                column: "id_employee_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Warning_id_employeed_fk",
                table: "Warning",
                column: "id_employeed_fk");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedule_id_employeer_fk",
                table: "WorkSchedule",
                column: "id_employeer_fk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenefitsEmployee");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "CurriculumCandidate");

            migrationBuilder.DropTable(
                name: "FilterCandidate");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "TrainingEmployee");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropTable(
                name: "Warning");

            migrationBuilder.DropTable(
                name: "WorkSchedule");

            migrationBuilder.DropTable(
                name: "Benefit");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Filter");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
