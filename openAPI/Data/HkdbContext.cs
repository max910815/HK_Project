using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using openAPI.Models;

namespace openAPI.Data;

public partial class HkdbContext : DbContext
{
    public HkdbContext()
    {
    }

    public HkdbContext(DbContextOptions<HkdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AiFile> AiFiles { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Embedding> Embeddings { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Qahistory> Qahistorys { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HKDB;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AiFile>(entity =>
        {
            entity.HasIndex(e => e.ApplicationId, "IX_AiFiles_ApplicationId");

            entity.HasOne(d => d.Application).WithMany(p => p.AiFiles).HasForeignKey(d => d.ApplicationId);
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasIndex(e => e.MemberId, "IX_Applications_MemberId");

            entity.HasOne(d => d.Member).WithMany(p => p.Applications).HasForeignKey(d => d.MemberId);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Chats_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Chats).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Embedding>(entity =>
        {
            entity.HasIndex(e => e.AifileId, "IX_Embeddings_AifileId");

            entity.HasOne(d => d.Aifile).WithMany(p => p.Embeddings).HasForeignKey(d => d.AifileId);
        });

        modelBuilder.Entity<Qahistory>(entity =>
        {
            entity.ToTable("QAHistorys");

            entity.HasIndex(e => e.ChatId, "IX_QAHistorys_ChatId");

            entity.HasOne(d => d.Chat).WithMany(p => p.Qahistories).HasForeignKey(d => d.ChatId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
