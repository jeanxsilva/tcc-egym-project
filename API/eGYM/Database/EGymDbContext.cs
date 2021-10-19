using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace eGYM.Models
{
    public partial class EGymDbContext : DbContext
    {
        public EGymDbContext()
        {
        }

        public EGymDbContext(DbContextOptions<EGymDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassCheckInOut> ClassCheckInOuts { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyUnit> CompanyUnits { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public virtual DbSet<LastNews> LastNews { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<ModalityClass> ModalityClasses { get; set; }
        public virtual DbSet<ModalityPaymentType> ModalityPaymentTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMovement> PaymentMovements { get; set; }
        public virtual DbSet<PaymentReversal> PaymentReversals { get; set; }
        public virtual DbSet<PaymentReversalStatus> PaymentReversalStatuses { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<PhysicalAssesment> PhysicalAssesments { get; set; }
        public virtual DbSet<PhysicalAssesmentScheduled> PhysicalAssesmentScheduleds { get; set; }
        public virtual DbSet<RegistrationModalityClass> RegistrationModalityClasses { get; set; }
        public virtual DbSet<RequestCategory> RequestCategories { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShiftBook> ShiftBooks { get; set; }
        public virtual DbSet<StudentCaracteristic> StudentCaracteristics { get; set; }
        public virtual DbSet<StudentRegistration> StudentRegistrations { get; set; }
        public virtual DbSet<StudentRequest> StudentRequests { get; set; }
        public virtual DbSet<TrainingPlan> TrainingPlans { get; set; }
        public virtual DbSet<TrainingPlanExercise> TrainingPlanExercises { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLevel> UserLevels { get; set; }
        public virtual DbSet<UserLevelAccess> UserLevelAccesses { get; set; }
        public virtual DbSet<UserLevelRole> UserLevelRoles { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserState> UserStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=egym_db;user=root;convertzerodatetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.18-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<ClassCheckInOut>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.StudentId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("class_check_in_out");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ModalityClassId, "fk_ClassCheckInOut_ModalityClass1_idx");

                entity.HasIndex(e => e.StudentId, "fk_ClassCheckInOut_Student1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StudentId).HasColumnType("int(11)");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModalityClassId).HasColumnType("int(11)");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ModalityClass)
                    .WithMany(p => p.ClassCheckInOuts)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ModalityClassId)
                    .HasConstraintName("fk_ClassCheckInOut_ModalityClass1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ClassCheckInOuts)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_ClassCheckInOut_Student1");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RegisterCode)
                    .IsRequired()
                    .HasMaxLength(14);
            });

            modelBuilder.Entity<CompanyUnit>(entity =>
            {
                entity.ToTable("company_unit");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyId, "fk_CompanyUnit_Company1_idx");

                entity.HasIndex(e => e.UserContactId, "fk_CompanyUnit_User1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CompanyId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.RegisterCode)
                    .IsRequired()
                    .HasMaxLength(14);

                entity.Property(e => e.UserContactId).HasColumnType("int(11)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyUnits)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_CompanyUnit_Company1");

                entity.HasOne(d => d.UserContact)
                    .WithMany(p => p.CompanyUnits)
                    .HasForeignKey(d => d.UserContactId)
                    .HasConstraintName("fk_CompanyUnit_User1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ShiftId, "fk_Employee_Shift1_idx");

                entity.HasIndex(e => e.UserId, "fk_Employee_User1_idx")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ShiftId).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Employee_Shift1");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Employee_User1");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ExerciseCategoryId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("exercise");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ExerciseCategoryId, "fk_Exercise_ExerciseCategory1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ExerciseCategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ExerciseCategory)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.ExerciseCategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Exercise_ExerciseCategory1");
            });

            modelBuilder.Entity<ExerciseCategory>(entity =>
            {
                entity.ToTable("exercise_category");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CompanyUnitId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("invoice");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyUnitId, "fk_Invoice_CompanyUnit1_idx");

                entity.HasIndex(e => e.InvoiceStatusId, "fk_Invoice_InvoiceStatus1_idx");

                entity.HasIndex(e => e.StudentId, "fk_Invoice_Student1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CompanyUnitId).HasColumnType("int(11)");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.InvoiceStatusId).HasColumnType("int(11)");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.ReferentToDate).HasColumnType("date");

                entity.Property(e => e.StudentId).HasColumnType("int(11)");

                entity.HasOne(d => d.CompanyUnit)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CompanyUnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Invoice_CompanyUnit1");

                entity.HasOne(d => d.InvoiceStatus)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceStatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Invoice_InvoiceStatus1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Invoice_Student1");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("invoice_details");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.InvoiceId, "fk_InvoiceDetails_Invoice1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_InvoiceDetails_Invoice1");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.ToTable("invoice_status");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LastNews>(entity =>
            {
                entity.ToTable("last_news");

                entity.HasIndex(e => e.PublishedByUserId, "fk_LastNews_user1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ExpireDateTime).HasColumnType("datetime");

                entity.Property(e => e.Options).HasMaxLength(255);

                entity.Property(e => e.PhotoUrl).HasMaxLength(255);

                entity.Property(e => e.PublishedByUserId).HasColumnType("int(11)");

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.PublishedByUser)
                    .WithMany(p => p.LastNews)
                    .HasForeignKey(d => d.PublishedByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_LastNews_user1");
            });

            modelBuilder.Entity<Modality>(entity =>
            {
                entity.ToTable("modalities");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DaysInWeek).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ModalityClass>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ModalityId, e.CompanyUnitId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("modality_class");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyUnitId, "fk_ModalityClass_CompanyUnit1_idx");

                entity.HasIndex(e => e.ModalityId, "fk_ModalityClass_Modalities1_idx");

                entity.HasIndex(e => e.InstructorId, "fk_ModalityClass_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModalityId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyUnitId).HasColumnType("int(11)");

                entity.Property(e => e.EndTime).HasColumnType("time");

                entity.Property(e => e.InstructorId).HasColumnType("int(11)");

                entity.Property(e => e.StartTime).HasColumnType("time");

                entity.Property(e => e.TotalActiveMembers).HasColumnType("int(11)");

                entity.Property(e => e.TotalVacancies).HasColumnType("int(11)");

                entity.HasOne(d => d.CompanyUnit)
                    .WithMany(p => p.ModalityClasses)
                    .HasForeignKey(d => d.CompanyUnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_ModalityClass_CompanyUnit1");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.ModalityClasses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("fk_ModalityClass_User1");

                entity.HasOne(d => d.Modality)
                    .WithMany(p => p.ModalityClasses)
                    .HasForeignKey(d => d.ModalityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_ModalityClass_Modalities1");
            });

            modelBuilder.Entity<ModalityPaymentType>(entity =>
            {
                entity.ToTable("modality_payment_type");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.InvoiceId, e.CompanyUnitId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("payment");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyUnitId, "fk_Payment_CompanyUnit1_idx");

                entity.HasIndex(e => e.InvoiceId, "fk_Payment_Invoice1_idx");

                entity.HasIndex(e => e.PaymentTypeId, "fk_Payment_PaymentType1_idx");

                entity.HasIndex(e => e.PaidByUserId, "fk_Payment_User1_idx");

                entity.HasIndex(e => e.ReceivedByUserId, "fk_Payment_User2_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.Property(e => e.CompanyUnitId).HasColumnType("int(11)");

                entity.Property(e => e.PaidByUserId).HasColumnType("int(11)");

                entity.Property(e => e.PaymentDateTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentTypeId).HasColumnType("int(11)");

                entity.Property(e => e.ReceivedByUserId).HasColumnType("int(11)");

                entity.HasOne(d => d.CompanyUnit)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CompanyUnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_CompanyUnit1");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Payments)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_Invoice1");

                entity.HasOne(d => d.PaidByUser)
                    .WithMany(p => p.PaymentPaidByUsers)
                    .HasForeignKey(d => d.PaidByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_User1");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_PaymentType1");

                entity.HasOne(d => d.ReceivedByUser)
                    .WithMany(p => p.PaymentReceivedByUsers)
                    .HasForeignKey(d => d.ReceivedByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Payment_User2");
            });

            modelBuilder.Entity<PaymentMovement>(entity =>
            {
                entity.ToTable("payment_movements");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RegisteredByEmployeeId, "fk_PaymentMovements_Employee1_idx");

                entity.HasIndex(e => e.PaymentReversalStatusId, "fk_PaymentMovements_PaymentReversalStatus1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.PaymentReversalStatusId).HasColumnType("int(11)");

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.RegisteredByEmployeeId).HasColumnType("int(11)");

                entity.HasOne(d => d.PaymentReversalStatus)
                    .WithMany(p => p.PaymentMovements)
                    .HasForeignKey(d => d.PaymentReversalStatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentMovements_PaymentReversalStatus1");

                entity.HasOne(d => d.RegisteredByEmployee)
                    .WithMany(p => p.PaymentMovements)
                    .HasForeignKey(d => d.RegisteredByEmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentMovements_Employee1");
            });

            modelBuilder.Entity<PaymentReversal>(entity =>
            {
                entity.ToTable("payment_reversal");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PaymentId, "fk_PaymentReversal_Payment1_idx");

                entity.HasIndex(e => e.PaymentReversalStatusId, "fk_PaymentReversal_PaymentReversalStatus1_idx");

                entity.HasIndex(e => e.AuthorizedByUserId, "fk_PaymentReversal_User1_idx");

                entity.HasIndex(e => e.CreatedByUserId, "fk_PaymentReversal_User2_idx");

                entity.HasIndex(e => e.FinishedByUserId, "fk_PaymentReversal_User3_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AuthorizedByUserId).HasColumnType("int(11)");

                entity.Property(e => e.CreatedByUserId).HasColumnType("int(11)");

                entity.Property(e => e.FinishedByUserId).HasColumnType("int(11)");

                entity.Property(e => e.LastModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnType("int(11)");

                entity.Property(e => e.PaymentReversalStatusId).HasColumnType("int(11)");

                entity.Property(e => e.Reason).HasMaxLength(255);

                entity.HasOne(d => d.AuthorizedByUser)
                    .WithMany(p => p.PaymentReversalAuthorizedByUsers)
                    .HasForeignKey(d => d.AuthorizedByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentReversal_User1");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.PaymentReversalCreatedByUsers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentReversal_User2");

                entity.HasOne(d => d.FinishedByUser)
                    .WithMany(p => p.PaymentReversalFinishedByUsers)
                    .HasForeignKey(d => d.FinishedByUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentReversal_User3");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PaymentReversals)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentReversal_Payment1");

                entity.HasOne(d => d.PaymentReversalStatus)
                    .WithMany(p => p.PaymentReversals)
                    .HasForeignKey(d => d.PaymentReversalStatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PaymentReversal_PaymentReversalStatus1");
            });

            modelBuilder.Entity<PaymentReversalStatus>(entity =>
            {
                entity.ToTable("payment_reversal_status");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("payment_type");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PhysicalAssesment>(entity =>
            {
                entity.ToTable("physical_assesment");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RegisteredByEmployeeId, "fk_PhysicalAssesment_Employee1_idx");

                entity.HasIndex(e => e.ScheduledPhysicalAssesmentId, "fk_PhysicalAssesment_PhysicalAssesmentScheduled1_idx");

                entity.HasIndex(e => e.StudentId, "fk_PhysicalAssesment_Student1_idx");

                entity.HasIndex(e => e.StudentCaracteristicsId, "fk_physical_assesment_StudentCaracteristics1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.RegisteredByEmployeeId).HasColumnType("int(11)");

                entity.Property(e => e.ScheduledPhysicalAssesmentId).HasColumnType("int(11)");

                entity.Property(e => e.StudentCaracteristicsId).HasColumnType("int(11)");

                entity.Property(e => e.StudentGoal).HasMaxLength(45);

                entity.Property(e => e.StudentId).HasColumnType("int(11)");

                entity.HasOne(d => d.RegisteredByEmployee)
                    .WithMany(p => p.PhysicalAssesments)
                    .HasForeignKey(d => d.RegisteredByEmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PhysicalAssesment_Employee1");

                entity.HasOne(d => d.ScheduledPhysicalAssesment)
                    .WithMany(p => p.PhysicalAssesments)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ScheduledPhysicalAssesmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PhysicalAssesment_PhysicalAssesmentScheduled1");

                entity.HasOne(d => d.StudentCaracteristics)
                    .WithMany(p => p.PhysicalAssesments)
                    .HasForeignKey(d => d.StudentCaracteristicsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_physical_assesment_StudentCaracteristics1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.PhysicalAssesments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PhysicalAssesment_Student1");
            });

            modelBuilder.Entity<PhysicalAssesmentScheduled>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.StudentRegistrationId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("physical_assesment_scheduled");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StudentRegistrationId, "fk_PhysicalAssesmentScheduled_StudentRegistration1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StudentRegistrationId).HasColumnType("int(11)");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.ScheduledToDate).HasColumnType("datetime");

                entity.HasOne(d => d.StudentRegistration)
                    .WithMany(p => p.PhysicalAssesmentScheduleds)
                    .HasForeignKey(d => d.StudentRegistrationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_PhysicalAssesmentScheduled_StudentRegistration1");
            });

            modelBuilder.Entity<RegistrationModalityClass>(entity =>
            {
                entity.ToTable("registration_modality_class");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ModalityClassId, "fk_Matricula_has_ModalityClass_ModalityClass1_idx");

                entity.HasIndex(e => e.StudentRegistrationId, "fk_RegistrationModalityClass_Student1_idx");

                entity.HasIndex(e => e.InvoiceId, "fk_registration_modality_class_invoice1_idx");

                entity.HasIndex(e => e.ModalityPaymentTypeId, "fk_registrationmodalityclass_modalitypaymenttype1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DueDay).HasColumnType("int(11)");

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.Property(e => e.ModalityClassId).HasColumnType("int(11)");

                entity.Property(e => e.ModalityPaymentTypeId).HasColumnType("int(11)");

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.StudentRegistrationId).HasColumnType("int(11)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.RegistrationModalityClasses)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("fk_registration_modality_class_invoice1");

                entity.HasOne(d => d.ModalityClass)
                    .WithMany(p => p.RegistrationModalityClasses)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ModalityClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Matricula_has_ModalityClass_ModalityClass1");

                entity.HasOne(d => d.ModalityPaymentType)
                    .WithMany(p => p.RegistrationModalityClasses)
                    .HasForeignKey(d => d.ModalityPaymentTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_registrationmodalityclass_modalitypaymenttype1");

                entity.HasOne(d => d.StudentRegistration)
                    .WithMany(p => p.RegistrationModalityClasses)
                    .HasForeignKey(d => d.StudentRegistrationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_RegistrationModalityClass_Student1");
            });

            modelBuilder.Entity<RequestCategory>(entity =>
            {
                entity.ToTable("request_category");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("request_status");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("shift");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EndTime).HasColumnType("time");

                entity.Property(e => e.StartTime).HasColumnType("time");
            });

            modelBuilder.Entity<ShiftBook>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.EmployeeId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("shift_book");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeId, "fk_ShiftMark_Employee1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeId).HasColumnType("int(11)");

                entity.Property(e => e.EntryDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExitDateTime).HasColumnType("datetime");

                entity.Property(e => e.ReferentToDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ShiftBooks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_ShiftMark_Employee1");
            });

            modelBuilder.Entity<StudentCaracteristic>(entity =>
            {
                entity.ToTable("student_caracteristics");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StudentRegistrationId, "fk_StudentCaracteristics_student_registration1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Abdominal).HasMaxLength(45);

                entity.Property(e => e.AgeAtMoment).HasColumnType("int(11)");

                entity.Property(e => e.Chest).HasMaxLength(45);

                entity.Property(e => e.Height)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.StudentRegistrationId).HasColumnType("int(11)");

                entity.Property(e => e.Subaxillary).HasMaxLength(45);

                entity.Property(e => e.Subscapular).HasMaxLength(45);

                entity.Property(e => e.Suprailiac).HasMaxLength(45);

                entity.Property(e => e.Thigh).HasMaxLength(45);

                entity.Property(e => e.Triceps).HasMaxLength(45);

                entity.Property(e => e.Weight)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.StudentRegistration)
                    .WithMany(p => p.StudentCaracteristics)
                    .HasForeignKey(d => d.StudentRegistrationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_StudentCaracteristics_student_registration1");
            });

            modelBuilder.Entity<StudentRegistration>(entity =>
            {
                entity.ToTable("student_registration");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ActualTrainingPlanId, "fk_Student_TrainingPlan1_idx");

                entity.HasIndex(e => e.UserId, "fk_Student_User1_idx")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ActualTrainingPlanId).HasColumnType("int(11)");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.ActualTrainingPlan)
                    .WithMany(p => p.StudentRegistrations)
                    .HasForeignKey(d => d.ActualTrainingPlanId)
                    .HasConstraintName("fk_Student_TrainingPlan1");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.StudentRegistration)
                    .HasForeignKey<StudentRegistration>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_Student_User1");
            });

            modelBuilder.Entity<StudentRequest>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.StudentId, e.RequestStatusId, e.RequestCategoryId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("student_requests");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RequestStatusId, "fk_RequestedPhysicalAssesments_RequestStatus1_idx");

                entity.HasIndex(e => e.StudentId, "fk_RequestedPhysicalAssesments_Student1_idx");

                entity.HasIndex(e => e.RequestCategoryId, "fk_StudentRequests_RequestCategory2_idx");

                entity.HasIndex(e => e.InvoiceId, "fk_studentrequests_invoice1_idx");

                entity.HasIndex(e => e.ReferToChangeModalityClassId, "fk_studentrequests_registrationmodalityclass1_idx");

                entity.HasIndex(e => e.ClosedByUserId, "fk_studentrequests_user1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StudentId).HasColumnType("int(11)");

                entity.Property(e => e.RequestStatusId).HasColumnType("int(11)");

                entity.Property(e => e.RequestCategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Attachment).HasMaxLength(100);

                entity.Property(e => e.ClosedByUserId).HasColumnType("int(11)");

                entity.Property(e => e.InvoiceId).HasColumnType("int(11)");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.ReferToChangeModalityClassId).HasColumnType("int(11)");

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClosedByUser)
                    .WithMany(p => p.StudentRequests)
                    .HasForeignKey(d => d.ClosedByUserId)
                    .HasConstraintName("fk_studentrequests_user1");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.StudentRequests)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("fk_studentrequests_invoice1");

                entity.HasOne(d => d.ReferToChangeModalityClass)
                    .WithMany(p => p.StudentRequests)
                    .HasForeignKey(d => d.ReferToChangeModalityClassId)
                    .HasConstraintName("fk_studentrequests_registrationmodalityclass1");

                entity.HasOne(d => d.RequestCategory)
                    .WithMany(p => p.StudentRequests)
                    .HasForeignKey(d => d.RequestCategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_StudentRequests_RequestCategory2");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.StudentRequests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_RequestedPhysicalAssesments_RequestStatus1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRequests)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_RequestedPhysicalAssesments_Student1");
            });

            modelBuilder.Entity<TrainingPlan>(entity =>
            {
                entity.ToTable("training_plan");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SpecificToStudentId, "fk_TrainingPlan_Student1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.RegisterDateTime).HasColumnType("datetime");

                entity.Property(e => e.SpecificToStudentId).HasColumnType("int(11)");

                entity.HasOne(d => d.SpecificToStudent)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.SpecificToStudentId)
                    .HasConstraintName("fk_TrainingPlan_Student1");
            });

            modelBuilder.Entity<TrainingPlanExercise>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TrainingPlanId, e.DayOfWeek, e.ExerciseId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("training_plan_exercises");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ExerciseId, "fk_Exercise_has_TrainingPlan_Exercise1_idx");

                entity.HasIndex(e => e.TrainingPlanId, "fk_Exercise_has_TrainingPlan_TrainingPlan1_idx");

                entity.HasIndex(e => e.CombinedExerciseId, "fk_TrainingPlanExercises_Exercise1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TrainingPlanId).HasColumnType("int(11)");

                entity.Property(e => e.DayOfWeek).HasColumnType("int(11)");

                entity.Property(e => e.ExerciseId).HasColumnType("int(11)");

                entity.Property(e => e.CombinedExerciseId).HasColumnType("int(11)");

                entity.Property(e => e.Order).HasColumnType("int(11)");

                entity.Property(e => e.Repetition)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.CombinedExercise)
                    .WithMany(p => p.TrainingPlanExerciseCombinedExercises)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.CombinedExerciseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_TrainingPlanExercises_Exercise1");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.TrainingPlanExerciseExercises)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ExerciseId)
                    .HasConstraintName("fk_Exercise_has_TrainingPlan_Exercise1");

                entity.HasOne(d => d.TrainingPlan)
                    .WithMany(p => p.TrainingPlanExercises)
                    .HasForeignKey(d => d.TrainingPlanId)
                    .HasConstraintName("fk_Exercise_has_TrainingPlan_TrainingPlan1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RegisterCode, "RegisterCode_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CompanyUnitId, "fk_User_CompanyUnit1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddressCity).HasMaxLength(45);

                entity.Property(e => e.AddressCode)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.AddressNumber).HasColumnType("int(11)");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CompanyUnitId).HasColumnType("int(11)");

                entity.Property(e => e.ContactPhone).HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.RegisterCode)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.CompanyUnit)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyUnitId)
                    .HasConstraintName("fk_User_CompanyUnit1");
            });

            modelBuilder.Entity<UserLevel>(entity =>
            {
                entity.ToTable("user_level");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleCode)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<UserLevelAccess>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserLevelId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_level_access");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserLevelId, "fk_UserLevelAccess_UserLevel1_idx");

                entity.HasIndex(e => e.ParentId, "fk_UserLevelAccess_UserLevelAccess1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserLevelId).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.IconKey).HasMaxLength(45);

                entity.Property(e => e.ParentId).HasColumnType("int(11)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("fk_UserLevelAccess_UserLevelAccess1");

                entity.HasOne(d => d.UserLevel)
                    .WithMany(p => p.UserLevelAccesses)
                    .HasForeignKey(d => d.UserLevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_UserLevelAccess_UserLevel1");
            });

            modelBuilder.Entity<UserLevelRole>(entity =>
            {
                entity.ToTable("user_level_roles");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserLevelId, "fk_UserLevelRoles_UserLevel1");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.UserLevelId).HasColumnType("int(11)");

                entity.HasOne(d => d.UserLevel)
                    .WithMany(p => p.UserLevelRoles)
                    .HasForeignKey(d => d.UserLevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_UserLevelRoles_UserLevel1");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("user_profile");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "Login_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "fk_UserProfile_User1_idx")
                    .IsUnique();

                entity.HasIndex(e => e.UserLevelId, "fk_UserProfile_UserLevel1_idx");

                entity.HasIndex(e => e.UserStateId, "fk_UserProfile_UserState1_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordEncrypted)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.UserLevelId).HasColumnType("int(11)");

                entity.Property(e => e.UserStateId).HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserProfile)
                    .HasForeignKey<UserProfile>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_UserProfile_User1");

                entity.HasOne(d => d.UserLevel)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserLevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_UserProfile_UserLevel1");

                entity.HasOne(d => d.UserState)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserStateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_UserProfile_UserState1");
            });

            modelBuilder.Entity<UserState>(entity =>
            {
                entity.ToTable("user_state");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
