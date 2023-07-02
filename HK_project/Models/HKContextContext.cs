using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HK_Product.Models
{
    public partial class HKContextContext : DbContext
    {
        public HKContextContext()
        {
        }

        public HKContextContext(DbContextOptions<HKContextContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aifile> Aifiles { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Embedding> Embeddings { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Qahistory> Qahistories { get; set; }

        public virtual DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=localhost;Database=HKContext;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Aifile>(entity =>
            {
                entity.ToTable("AIFiles");

                entity.HasIndex(e => e.ApplicationId, "IX_AIFiles_ApplicationId");

                entity.Property(e => e.AifileId).HasColumnName("AIFileId");

                entity.Property(e => e.AifilePath)
                    .IsRequired()
                    .HasColumnName("AIFilePath");

                entity.Property(e => e.AifileType)
                    .IsRequired()
                    .HasColumnName("AIFileType");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Aifiles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //modelBuilder.Entity<Application>(entity =>
            //{
            //    entity.HasIndex(e => e.MemberId, "IX_Applications_MemberId");

            //    entity.Property(e => e.Model).IsRequired();

            //    entity.Property(e => e.Parameter).IsRequired();

            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.Applications)
            //        .HasForeignKey(d => d.MemberId)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Chats_UserId");

                entity.Property(e => e.ChatName).IsRequired();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Embedding>(entity =>
            {
                entity.ToTable("Embedding");

                entity.HasIndex(e => e.AifileId, "IX_Embedding_AIFileId");

                entity.Property(e => e.AifileId).HasColumnName("AIFileId");

                entity.Property(e => e.EmbeddingAnswer).IsRequired();

                entity.Property(e => e.EmbeddingQuestion).IsRequired();

                entity.Property(e => e.EmbeddingVectors).IsRequired();

                entity.Property(e => e.Qa)
                    .IsRequired()
                    .HasColumnName("QA");

                entity.HasOne(d => d.Aifile)
                    .WithMany(p => p.Embeddings)
                    .HasForeignKey(d => d.AifileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberEmail).IsRequired();

                entity.Property(e => e.MemberName).IsRequired();

                entity.Property(e => e.MemberPassword).IsRequired();
            });

            modelBuilder.Entity<Qahistory>(entity =>
            {
                entity.ToTable("QAHistory");

                entity.HasIndex(e => e.ChatId, "IX_QAHistory_ChatId");

                entity.Property(e => e.QahistoryId).HasColumnName("QAHistoryId");

                entity.Property(e => e.QahistoryA)
                    .IsRequired()
                    .HasColumnName("QAHistoryA");

                entity.Property(e => e.QahistoryQ)
                    .IsRequired()
                    .HasColumnName("QAHistoryQ");

                entity.Property(e => e.QahistoryVectors)
                    .IsRequired()
                    .HasColumnName("QAHistoryVectors");

                entity.HasOne(d => d.Chat)
                    .WithMany(p => p.Qahistories)
                    .HasForeignKey(d => d.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Applications, "IX_Users_ApplicationId");

                entity.Property(e => e.UserEmail).IsRequired();

                entity.Property(e => e.UserName).IsRequired();

                entity.Property(e => e.UserPassword).IsRequired();

            });

           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
