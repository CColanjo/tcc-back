
using Audit.Core;
using Audit.EntityFramework;
using schedule_appointment_domain.Helpers;
using schedule_appointment_domain.Model;
using schedule_appointment_domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace schedule_appointment_infra
{
    public class ApplicationDbContext : AuditDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUser user) : base(options)
        {
            Audit.Core.Configuration.Setup();      
                 
        }

       
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Schedule> Schedule { get; set; } = default!;
        public DbSet<Professional> Professional { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable(nameof(Event));

                entity.HasKey(e => e.Id);

                entity.Property(e => e.PrimaryKey).HasColumnType("varchar(100)").IsRequired();
                entity.Property(e => e.Table).HasColumnType("varchar(150)").IsRequired();
                entity.Property(e => e.Action).HasColumnType("varchar(150)").IsRequired();
                entity.Property(e => e.ColumnValues).IsRequired(false);
                entity.Property(e => e.Changes).IsRequired(false);
                entity.Property(e => e.Environment).IsRequired(false);
                entity.Property(e => e.User).IsRequired();
                entity.Property(e => e.UpdatedDate).HasColumnType("timestamp without time zone");
            });

            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(150).IsRequired();

                entity.Property(e => e.Active).IsRequired();
                entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone").IsRequired();
            });
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

