using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hostman.Database
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Authentication> Authentication { get; set; }
        public virtual DbSet<Host> Host { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VpnAuthentication> VpnAuthentication { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("authentication");

                entity.HasIndex(e => new { e.Issuer, e.Subject })
                    .HasName("Issuer_And_Subject")
                    .IsUnique();

                entity.HasIndex(e => new { e.UserId, e.Issuer })
                    .HasName("User_And_Issuer")
                    .IsUnique();

                entity.Property(e => e.Issuer)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UserId).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("User");
            });

            modelBuilder.Entity<Host>(entity =>
            {
                entity.ToTable("host");

                entity.HasIndex(e => e.AssignedIp)
                    .HasName("AssignedIP")
                    .IsUnique();

                entity.HasIndex(e => new { e.Owner, e.Name })
                    .HasName("Owner_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AssignedIp)
                    .HasColumnName("AssignedIP")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Ipmode)
                    .IsRequired()
                    .HasColumnName("IPMode")
                    .HasColumnType("enum('Static','Dynamic')")
                    .HasDefaultValueSql("'Dynamic'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Owner).HasColumnType("int(10) unsigned");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Host)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("Owner");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Nickname)
                    .HasName("Nickname")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<VpnAuthentication>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vpn_authentication");

                entity.HasIndex(e => e.HostId)
                    .HasName("HostId")
                    .IsUnique();

                entity.Property(e => e.Expiration)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'addtime(current_timestamp(),''0:10:0'')'");

                entity.Property(e => e.HostId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasComment(@"Does not need to be hashed/salted;
only temporary and for machine-only
communication.")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Host)
                    .WithOne()
                    .HasForeignKey<VpnAuthentication>(d => d.HostId)
                    .HasConstraintName("HostId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
