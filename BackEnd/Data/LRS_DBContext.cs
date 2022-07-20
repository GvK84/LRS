using Microsoft.EntityFrameworkCore;



#nullable disable

namespace BackEnd.Data
{
    public partial class LRS_DBContext : DbContext
    {

        /// <summary>Initializes a new instance of the <see cref="LRS_DBContext" /> class.</summary>
        public LRS_DBContext()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LRS_DBContext" /> class.</summary>
        /// <param name="options">The options.</param>
        public LRS_DBContext(DbContextOptions<LRS_DBContext> options)
            : base(options)
        {
        }
        /// <summary>
        ///   <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        ///   <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions">DbContextOptions</see> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured">IsConfigured</see> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)">OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)</see>.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">
        /// A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.
        /// </param>
        ///
        /// TODO not needed
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder
        // .UseSqlServer(ConnectionService.connstring)
        // .UseLazyLoadingProxies();

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTitle> UserTitles { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1">DbSet</see> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.
        /// </param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)">UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)</see>)
        /// then this method will not be run.
        /// </remarks>
        /// TODO we have a database first approach so no need for model creating
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