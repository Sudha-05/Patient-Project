using GestorPacientes.Core.Domain.Common;
using GestorPacientes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestorPacientes.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
        public DbSet<LaboratoryResult> LaboratoryResults { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API

            #region tables
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<LaboratoryTest>().ToTable("LaboratoryTests");
            modelBuilder.Entity<LaboratoryResult>().ToTable("LaboratoryResults");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
            modelBuilder.Entity<Patient>().HasKey(p => p.Id);
            modelBuilder.Entity<LaboratoryTest>().HasKey(lt => lt.Id);
            modelBuilder.Entity<LaboratoryResult>().HasKey(lr => lr.Id);
            modelBuilder.Entity<Appointment>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Doctor>()
                .HasMany<Appointment>(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(ap => ap.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasMany<LaboratoryResult>(a => a.LaboratoryResults)
                .WithOne(lr => lr.Appointment)
                .HasForeignKey(lr => lr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LaboratoryTest>()
                .HasMany<LaboratoryResult>(lt => lt.LaboratoryResults)
                .WithOne(lr => lr.LaboratoryTest)
                .HasForeignKey(lr => lr.LaboratoryTestId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<User>()
            //    .HasMany<Patient>(user => user.Patients)
            //    .WithOne(p => p.User)
            //    .HasForeignKey(patient => patient.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany<Appointment>(user => user.Appointments)
            //    .WithOne(a => a.User)
            //    .HasForeignKey(appointment => appointment.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany<LaboratoryResult>(user => user.LaboratoryResults)
            //    .WithOne(lr => lr.User)
            //    .HasForeignKey(lr => lr.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region "property configuration"

            #region doctor
            modelBuilder.Entity<Doctor>().Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Doctor>().Property(d => d.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Doctor>().Property(d => d.Email).IsRequired();
            modelBuilder.Entity<Doctor>().Property(d => d.Phone).IsRequired();
            modelBuilder.Entity<Doctor>().Property(d => d.IdNumber).IsRequired();
            #endregion

            #region patient
            modelBuilder.Entity<Patient>().Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Patient>().Property(d => d.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Patient>().Property(d => d.Phone).IsRequired();
            modelBuilder.Entity<Patient>().Property(d => d.Address).IsRequired();
            modelBuilder.Entity<Patient>().Property(d => d.IdNumber).IsRequired();
            modelBuilder.Entity<Patient>().Property(d => d.DateBirth).IsRequired();
            modelBuilder.Entity<Patient>().Property(d => d.IsSmoker).IsRequired();
            modelBuilder.Entity<Patient>().Property(d => d.HasAllergies).IsRequired();
            #endregion

            #region "laboratory test"
            modelBuilder.Entity<LaboratoryTest>().Property(lt => lt.Name).IsRequired().HasMaxLength(100);
            #endregion

            #region appointment
            modelBuilder.Entity<Appointment>().Property(lt => lt.Day).IsRequired();
            modelBuilder.Entity<Appointment>().Property(lt => lt.Time).IsRequired();
            modelBuilder.Entity<Appointment>().Property(lt => lt.Reason).IsRequired();
            modelBuilder.Entity<Appointment>().Property(lt => lt.status).IsRequired();
            #endregion
            #region users
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(user => user.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(user => user.Username).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Password).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Email).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Phone).IsRequired();
            #endregion

            #endregion
        }
    }
}
