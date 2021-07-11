using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class BackEndContext : DbContext
    {
        public BackEndContext()
        {
        }

        public BackEndContext(DbContextOptions<BackEndContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Discusstion> Discusstions { get; set; }
        public virtual DbSet<DiscusstionComment> DiscusstionComments { get; set; }
        public virtual DbSet<DiscusstionFollower> DiscusstionFollowers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductComment> ProductComments { get; set; }
        public virtual DbSet<ProductFollower> ProductFollowers { get; set; }
        public virtual DbSet<ProductHistory> ProductHistories { get; set; }
        public virtual DbSet<ProductHistoryMedia> ProductHistoryMedias { get; set; }
        public virtual DbSet<ProductMedia> ProductMedias { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollower> UserFollowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=34.80.165.225;Database=BackEnd;user=Jack;password=BackEnd8588;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Discusstion>(entity =>
            {
                entity.ToTable("Discusstion");

                entity.Property(e => e.DiscusstionContent)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.DiscusstionDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscusstionTitle)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.DiscusstonCategory).HasMaxLength(32);
            });

            modelBuilder.Entity<DiscusstionComment>(entity =>
            {
                entity.ToTable("DiscusstionComment");

                entity.Property(e => e.DiscusstionCommentContent)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.DiscusstionCommentDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<DiscusstionFollower>(entity =>
            {
                entity.ToTable("DiscusstionFollower");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductCategory)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.ProductContent)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ProductDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductHeadShotPath)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductSubtitle).HasMaxLength(16);

                entity.Property(e => e.ProductTitle)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ProductUrl)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("ProductURL");
            });

            modelBuilder.Entity<ProductComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("ProductComment");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CommentDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProductFollower>(entity =>
            {
                entity.ToTable("ProductFollower");
            });

            modelBuilder.Entity<ProductHistory>(entity =>
            {
                entity.ToTable("ProductHistory");

                entity.Property(e => e.ProductHistoryContent)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ProductHistoryHeadShot)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryTitle)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ProductHistoryVersion)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.ProductHistorytDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProductHistoryMedia>(entity =>
            {
                entity.ToTable("ProductHistoryMedia");

                entity.Property(e => e.ProductHistoryMediaPathFive)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryMediaPathFour)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryMediaPathOne)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryMediaPathThree)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryMediaPathTwo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductHistoryMediaVideoPath)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductMedia>(entity =>
            {
                entity.ToTable("ProductMedia");

                entity.Property(e => e.ProductMediaPathFive)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMediaPathFour)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMediaPathOne)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMediaPathThree)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMediaPathTwo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ProductMediaVideoPath)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserAccount)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UserBackgroundColor)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UserDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserHeadShotPath)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UserIntroduction)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserRealName)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.ToTable("UserFollower");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
