using Microsoft.EntityFrameworkCore;
using openAPI.Models;

namespace openAPI.Data;

public partial class HKContext : DbContext
{
    public HKContext()
    {
    }

    public HKContext(DbContextOptions<HKContext> options)
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
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HKContext;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aifile>(entity =>
        {
            entity.ToTable("AIFiles");

            entity.HasIndex(e => e.ApplicationId, "IX_AIFiles_ApplicationId");

            entity.Property(e => e.AifileId).HasColumnName("AIFileId");
            entity.Property(e => e.AifilePath).HasColumnName("AIFilePath");
            entity.Property(e => e.AifileType).HasColumnName("AIFileType");

            entity.HasOne(d => d.Application).WithMany(p => p.Aifile)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasIndex(e => e.MemberId, "IX_Applications_MemberId");

            entity.HasOne(d => d.Member).WithMany(p => p.Application)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Chats_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Chat).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Embedding>(entity =>
        {
            entity.ToTable("Embedding");

            entity.HasIndex(e => e.AifileId, "IX_Embedding_AIFileId");

            entity.Property(e => e.AifileId).HasColumnName("AIFileId");
            entity.Property(e => e.Qa).HasColumnName("QA");

            entity.HasOne(d => d.Aifile).WithMany(p => p.Embeddings)
                .HasForeignKey(d => d.AifileId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");
        });

        modelBuilder.Entity<Qahistory>(entity =>
        {
            entity.ToTable("QAHistory");

            entity.HasIndex(e => e.ChatId, "IX_QAHistory_ChatId");

            entity.Property(e => e.QahistoryId).HasColumnName("QAHistoryId");
            entity.Property(e => e.QahistoryA).HasColumnName("QAHistoryA");
            entity.Property(e => e.QahistoryQ).HasColumnName("QAHistoryQ");
            entity.Property(e => e.QahistoryVectors).HasColumnName("QAHistoryVectors");

            entity.HasOne(d => d.Chat).WithMany(p => p.Qahistorie)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.ApplicationId, "IX_Users_ApplicationId");

            entity.HasOne(d => d.Application).WithMany(p => p.User)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
