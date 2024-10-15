using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ERP_API.Models;

public partial class DbAae441Dbaabc42erpContext : DbContext
{
    public DbAae441Dbaabc42erpContext()
    {
    }

    public DbAae441Dbaabc42erpContext(DbContextOptions<DbAae441Dbaabc42erpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<BenefitsEmployee> BenefitsEmployees { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Curriculum> Curricula { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Filter> Filters { get; set; }

    public virtual DbSet<FilterCandidate> FilterCandidates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    public virtual DbSet<TrainingEmployee> TrainingEmployees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Vacation> Vacations { get; set; }

    public virtual DbSet<Warning> Warnings { get; set; }

    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql5106.site4now.net,1433;Database=db_aae441_dbaabc42erp;User Id=db_aae441_dbaabc42erp_admin;Password=ERPROOT123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.IdBenefits);

            entity.ToTable("Benefit");

            entity.Property(e => e.IdBenefits).HasColumnName("id_benefits");
            entity.Property(e => e.DescriptionBenefits)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("description_benefits");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.NameBenefits)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_benefits");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Benefits).HasForeignKey(d => d.IdCompanyFk);
        });

        modelBuilder.Entity<BenefitsEmployee>(entity =>
        {
            entity.HasKey(e => e.IdBenefitsEmployee);

            entity.ToTable("BenefitsEmployee");

            entity.HasIndex(e => e.IdBenefitsFk, "IX_BenefitsEmployee_id_benefits_fk");

            entity.HasIndex(e => e.IdEmployeeFk, "IX_BenefitsEmployee_id_employee_fk");

            entity.Property(e => e.IdBenefitsEmployee).HasColumnName("id_benefits_employee");
            entity.Property(e => e.IdBenefitsFk).HasColumnName("id_benefits_fk");
            entity.Property(e => e.IdEmployeeFk).HasColumnName("id_employee_fk");

            entity.HasOne(d => d.IdBenefitsFkNavigation).WithMany(p => p.BenefitsEmployees)
                .HasForeignKey(d => d.IdBenefitsFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BenefitsEmployee_Benefit");

            entity.HasOne(d => d.IdEmployeeFkNavigation).WithMany(p => p.BenefitsEmployees)
                .HasForeignKey(d => d.IdEmployeeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BenefitsEmployee_Employee");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.IdCandidate).HasName("PK__Candidat__3CD1A87F17A41FDF");

            entity.ToTable("Candidate");

            entity.HasIndex(e => e.IdPersonFk, "IX_Candidate_id_person_fk");

            entity.Property(e => e.IdCandidate).HasColumnName("id_candidate");
            entity.Property(e => e.ApplicationDateCandidate)
                .HasColumnType("datetime")
                .HasColumnName("application_date_candidate");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdPersonFk).HasColumnName("id_person_fk");
            entity.Property(e => e.PositionAppliedCandidate).HasColumnName("position_applied_candidate");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Candidates).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdPersonFkNavigation).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.IdPersonFk)
                .HasConstraintName("FK__Candidate__id_pe__6EF57B66");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.IdCompany).HasName("PK__Company__5D0E9F0690327F24");

            entity.ToTable("Company");

            entity.HasIndex(e => e.CodeCompany, "UQ__Company__A8E62A4EB7995DF6").IsUnique();

            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.CodeCompany)
                .HasMaxLength(1000)
                .HasColumnName("code_company");
            entity.Property(e => e.DescriptionCompany)
                .HasMaxLength(1000)
                .HasColumnName("description_company");
            entity.Property(e => e.LocationCompany)
                .HasMaxLength(1000)
                .HasColumnName("location_company");
            entity.Property(e => e.NameCompany)
                .HasMaxLength(200)
                .HasColumnName("name_company");
            entity.Property(e => e.StatusCompany).HasColumnName("status_company");
            entity.Property(e => e.UrlCompany)
                .HasMaxLength(1000)
                .HasColumnName("url_company");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.IdCurriculum).HasName("PK__Curricul__8151415FCF29CF7D");

            entity.ToTable("Curriculum");

            entity.HasIndex(e => e.IdCandidateFk, "IX_Curriculum_id_candidate_fk");

            entity.HasIndex(e => e.IdEmployeeFk, "IX_Curriculum_id_employee_fk");

            entity.Property(e => e.IdCurriculum).HasColumnName("id_curriculum");
            entity.Property(e => e.DateUploaded)
                .HasColumnType("date")
                .HasColumnName("date_uploaded");
            entity.Property(e => e.IdCandidateFk).HasColumnName("id_candidate_fk");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdEmployeeFk).HasColumnName("id_employee_fk");
            entity.Property(e => e.PathFileCurriculum)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("path_file_curriculum");

            entity.HasOne(d => d.IdCandidateFkNavigation).WithMany(p => p.Curricula).HasForeignKey(d => d.IdCandidateFk);

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Curricula).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdEmployeeFkNavigation).WithMany(p => p.Curricula)
                .HasForeignKey(d => d.IdEmployeeFk)
                .HasConstraintName("FK__Curriculu__id_em__4D94879B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__F807679C158F656B");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.IdUserFk, "IX_Employee_id_user_fk");

            entity.Property(e => e.IdEmployee).HasColumnName("id_employee");
            entity.Property(e => e.DepartmentEmployee)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("department_employee");
            entity.Property(e => e.HiringDateEmployee)
                .HasColumnType("date")
                .HasColumnName("hiring_date_employee");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdUserFk).HasColumnName("id_user_fk");
            entity.Property(e => e.NetSalaryEmployee).HasColumnName("net_salary_employee");
            entity.Property(e => e.PositionEmployee)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("position_employee");
            entity.Property(e => e.VacationsEmployee).HasColumnName("vacations_employee");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Employees).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdUserFkNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdUserFk)
                .HasConstraintName("FK__Employee__id_use__3C69FB99");
        });

        modelBuilder.Entity<Filter>(entity =>
        {
            entity.HasKey(e => e.IdFilter).HasName("PK__Filter__F614A2C5F995FDDE");

            entity.ToTable("Filter");

            entity.Property(e => e.IdFilter).HasColumnName("id_filter");
            entity.Property(e => e.DateFilter)
                .HasColumnType("datetime")
                .HasColumnName("date_filter");
            entity.Property(e => e.DescriptionFilter).HasColumnName("description_filter");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.NameFilter)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_filter");
            entity.Property(e => e.ObservationAboutCandidate).HasColumnName("observation_about_candidate");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Filters).HasForeignKey(d => d.IdCompanyFk);
        });

        modelBuilder.Entity<FilterCandidate>(entity =>
        {
            entity.HasKey(e => e.IdFilterCandidate).HasName("PK__FilterCa__3808217E2FEF49C3");

            entity.ToTable("FilterCandidate");

            entity.HasIndex(e => e.IdCandidateFk, "IX_FilterCandidate_id_candidate_fk");

            entity.HasIndex(e => e.IdFilterFk, "IX_FilterCandidate_id_filter_fk");

            entity.Property(e => e.IdFilterCandidate).HasColumnName("id_filter_candidate");
            entity.Property(e => e.IdCandidateFk).HasColumnName("id_candidate_fk");
            entity.Property(e => e.IdFilterFk).HasColumnName("id_filter_fk");

            entity.HasOne(d => d.IdCandidateFkNavigation).WithMany(p => p.FilterCandidates)
                .HasForeignKey(d => d.IdCandidateFk)
                .HasConstraintName("FK__FilterCan__id_ca__778AC167");

            entity.HasOne(d => d.IdFilterFkNavigation).WithMany(p => p.FilterCandidates)
                .HasForeignKey(d => d.IdFilterFk)
                .HasConstraintName("FK__FilterCan__id_fi__76969D2E");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification);

            entity.ToTable("Notification");

            entity.HasIndex(e => e.IdEmployeedFk, "IX_Notification_id_employeed_fk");

            entity.Property(e => e.IdNotification).HasColumnName("id_notification");
            entity.Property(e => e.DateNotification)
                .HasColumnType("datetime")
                .HasColumnName("date_notification");
            entity.Property(e => e.DescriptionNotification).HasColumnName("description_notification");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdEmployeedFk).HasColumnName("id_employeed_fk");
            entity.Property(e => e.StatusNotification).HasColumnName("status_notification");
            entity.Property(e => e.TypeNameNotification)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("type_name_notification");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Notifications).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdEmployeedFkNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdEmployeedFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_Employee");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.IdPermission).HasName("PK__Permissi__5180B3BF93E2630D");

            entity.ToTable("Permission");

            entity.Property(e => e.IdPermission).HasColumnName("id_permission");
            entity.Property(e => e.DescriptionPermission)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("description_permission");
            entity.Property(e => e.NamePermission)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_permission");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson).HasName("PK__Person__E9AB6A419FC3C7D6");

            entity.ToTable("Person");

            entity.HasIndex(e => e.IdCompanyFk, "IX_Person_id_company_fk");

            entity.Property(e => e.IdPerson).HasColumnName("id_person");
            entity.Property(e => e.AddressPerson)
                .HasMaxLength(100)
                .HasColumnName("address_person");
            entity.Property(e => e.AgePerson).HasColumnName("age_person");
            entity.Property(e => e.EmailPerson)
                .HasMaxLength(100)
                .HasColumnName("email_person");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdentificationPerson)
                .HasMaxLength(100)
                .HasColumnName("identification_person");
            entity.Property(e => e.LastNamePerson)
                .HasMaxLength(100)
                .HasColumnName("last_name_person");
            entity.Property(e => e.NamePerson)
                .HasMaxLength(100)
                .HasColumnName("name_person");
            entity.Property(e => e.NationalityPerson)
                .HasMaxLength(100)
                .HasColumnName("nationality_person");
            entity.Property(e => e.PhoneNumberPerson)
                .HasMaxLength(100)
                .HasColumnName("phone_number_person");
            entity.Property(e => e.SecondLastNamePerson)
                .HasMaxLength(100)
                .HasColumnName("second_last_name_person");
            entity.Property(e => e.StatePerson).HasColumnName("state_person");
            entity.Property(e => e.UuidPerson).HasColumnName("UUID_person");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdCompanyFk)
                .HasConstraintName("FK_Person_Company");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__3D48441D62B42DA8");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.DescriptionRole).HasColumnName("Description_role");
            entity.Property(e => e.TypeRole)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("type_role");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.IdRolePermission).HasName("PK__RolePerm__B8BA225BE4556F0E");

            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.IdPermissionFk, "IX_RolePermission_id_permission_fk");

            entity.HasIndex(e => e.IdRoleFk, "IX_RolePermission_id_role_fk");

            entity.Property(e => e.IdRolePermission).HasColumnName("id_role_permission");
            entity.Property(e => e.IdPermissionFk).HasColumnName("id_permission_fk");
            entity.Property(e => e.IdRoleFk).HasColumnName("id_role_fk");

            entity.HasOne(d => d.IdPermissionFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.IdPermissionFk)
                .HasConstraintName("FK__RolePermi__id_pe__4AB81AF0");

            entity.HasOne(d => d.IdRoleFkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.IdRoleFk)
                .HasConstraintName("FK__RolePermi__id_ro__49C3F6B7");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PK__Session__A9E494D0F73F88E4");

            entity.ToTable("Session");

            entity.HasIndex(e => e.IdUserFk, "IX_Session_id_user_fk");

            entity.Property(e => e.IdSession).HasColumnName("id_session");
            entity.Property(e => e.CreationDateSession)
                .HasColumnType("datetime")
                .HasColumnName("creation_date_session");
            entity.Property(e => e.ExpirationDateSession)
                .HasColumnType("datetime")
                .HasColumnName("expiration_date_session");
            entity.Property(e => e.IdUserFk).HasColumnName("id_user_fk");
            entity.Property(e => e.StatusSession).HasColumnName("status_session");
            entity.Property(e => e.TokenSession)
                .HasMaxLength(1000)
                .IsFixedLength()
                .HasColumnName("token_session");
            entity.Property(e => e.UpdateDateSession)
                .HasColumnType("datetime")
                .HasColumnName("update_date_session");

            entity.HasOne(d => d.IdUserFkNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.IdUserFk)
                .HasConstraintName("FK__Session__id_user__3F466844");
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.IdTraining).HasName("PK_training");

            entity.Property(e => e.IdTraining).HasColumnName("id_training");
            entity.Property(e => e.DescriptionTraining)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("description_training");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.NameTraining)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_training");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Training).HasForeignKey(d => d.IdCompanyFk);
        });

        modelBuilder.Entity<TrainingEmployee>(entity =>
        {
            entity.HasKey(e => e.IdTrainingEmployee);

            entity.ToTable("TrainingEmployee");

            entity.HasIndex(e => e.IdEmployeeFk, "IX_TrainingEmployee_id_employee_fk");

            entity.HasIndex(e => e.IdTrainingFk, "IX_TrainingEmployee_id_training_fk");

            entity.Property(e => e.IdTrainingEmployee).HasColumnName("id_training_employee");
            entity.Property(e => e.IdEmployeeFk).HasColumnName("id_employee_fk");
            entity.Property(e => e.IdTrainingFk).HasColumnName("id_training_fk");

            entity.HasOne(d => d.IdEmployeeFkNavigation).WithMany(p => p.TrainingEmployees)
                .HasForeignKey(d => d.IdEmployeeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEmployee_Employee");

            entity.HasOne(d => d.IdTrainingFkNavigation).WithMany(p => p.TrainingEmployees)
                .HasForeignKey(d => d.IdTrainingFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainingEmployee_training");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__User__D2D14637BD867DE2");

            entity.ToTable("User");

            entity.HasIndex(e => e.IdPersonFk, "IX_User_id_person_fk");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.CreationDateUser)
                .HasColumnType("datetime")
                .HasColumnName("creation_date_user");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdPersonFk).HasColumnName("id_person_fk");
            entity.Property(e => e.NameUser)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_user");
            entity.Property(e => e.PasswordUser)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("password_user");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Users).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdPersonFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPersonFk)
                .HasConstraintName("FK__User__id_person___398D8EEE");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdUserRole).HasName("PK__UserRole__4FD2ABB33BD08450");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.IdRoleFk, "IX_UserRole_id_role_fk");

            entity.HasIndex(e => e.IdUserFk, "IX_UserRole_id_user_fk");

            entity.Property(e => e.IdUserRole).HasColumnName("id_user_role");
            entity.Property(e => e.IdRoleFk).HasColumnName("id_role_fk");
            entity.Property(e => e.IdUserFk).HasColumnName("id_user_fk");

            entity.HasOne(d => d.IdRoleFkNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdRoleFk)
                .HasConstraintName("FK__UserRole__id_rol__440B1D61");

            entity.HasOne(d => d.IdUserFkNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdUserFk)
                .HasConstraintName("FK__UserRole__id_use__44FF419A");
        });

        modelBuilder.Entity<Vacation>(entity =>
        {
            entity.HasKey(e => e.IdVacations).HasName("PK__Vacation__57B18186994EE17E");

            entity.ToTable("Vacation");

            entity.HasIndex(e => e.IdEmployeeFk, "IX_Vacation_id_employee_fk");

            entity.Property(e => e.IdVacations).HasColumnName("id_vacations");
            entity.Property(e => e.Approved).HasColumnName("approved");
            entity.Property(e => e.DaysTaken).HasColumnName("days_taken");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdEmployeeFk).HasColumnName("id_employee_fk");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Vacations).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdEmployeeFkNavigation).WithMany(p => p.Vacations)
                .HasForeignKey(d => d.IdEmployeeFk)
                .HasConstraintName("FK__Vacations__id_em__5070F446");
        });

        modelBuilder.Entity<Warning>(entity =>
        {
            entity.HasKey(e => e.IdWarning);

            entity.ToTable("Warning");

            entity.HasIndex(e => e.IdEmployeedFk, "IX_Warning_id_employeed_fk");

            entity.Property(e => e.IdWarning).HasColumnName("id_warning");
            entity.Property(e => e.DateWarning)
                .HasColumnType("datetime")
                .HasColumnName("date_warning");
            entity.Property(e => e.DescrptionWarning)
                .HasMaxLength(2000)
                .IsFixedLength()
                .HasColumnName("descrption_warning");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdEmployeedFk).HasColumnName("id_employeed_fk");
            entity.Property(e => e.ReasonWarning)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("reason_warning");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.Warnings).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdEmployeedFkNavigation).WithMany(p => p.Warnings)
                .HasForeignKey(d => d.IdEmployeedFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warning_Employee");
        });

        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.IdWorkSchedule).HasName("PK_workSchedule");

            entity.ToTable("WorkSchedule");

            entity.HasIndex(e => e.IdEmployeerFk, "IX_WorkSchedule_id_employeer_fk");

            entity.Property(e => e.IdWorkSchedule).HasColumnName("id_work_schedule");
            entity.Property(e => e.EndWorkSchedule)
                .HasColumnType("datetime")
                .HasColumnName("end_work_schedule");
            entity.Property(e => e.IdCompanyFk).HasColumnName("id_company_fk");
            entity.Property(e => e.IdEmployeerFk).HasColumnName("id_employeer_fk");
            entity.Property(e => e.NameWorkSchedule)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("name_work_schedule");
            entity.Property(e => e.StartWorkSchedule)
                .HasColumnType("datetime")
                .HasColumnName("start_work_schedule");

            entity.HasOne(d => d.IdCompanyFkNavigation).WithMany(p => p.WorkSchedules).HasForeignKey(d => d.IdCompanyFk);

            entity.HasOne(d => d.IdEmployeerFkNavigation).WithMany(p => p.WorkSchedules)
                .HasForeignKey(d => d.IdEmployeerFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_workSchedule_Employee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
