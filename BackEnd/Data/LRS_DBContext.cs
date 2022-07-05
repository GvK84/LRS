using Microsoft.EntityFrameworkCore;



#nullable disable

namespace BackEnd.Data
{
    public partial class LRS_DBContext : DbContext
    {

        public LRS_DBContext()
        {
        }

        public LRS_DBContext(DbContextOptions<LRS_DBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer(ConnectionService.connstring)
        .UseLazyLoadingProxies();


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTitle> UserTitles { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Surname).HasMaxLength(20);

                entity.HasOne(d => d.UserTitle)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_ToUserTitle");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_ToUserType");

            });

            modelBuilder.Entity<UserTitle>(entity =>
            {
                entity.ToTable("UserTitle");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
