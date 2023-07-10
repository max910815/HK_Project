using HKDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace HKDB.Data
{
    public class HKContext : DbContext
    {
        public HKContext()
        {
        }

        public HKContext(DbContextOptions<HKContext> options) : base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Aifile> AiFiles { get; set; }
        public DbSet<Embedding> Embeddings { get; set; }
        public DbSet<Qahistory> QAHistorys { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HKDB;TrustServerCertificate=true;MultipleActiveResultSets=true;Trusted_Connection=True;");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(options =>
            {
                options.HasOne(m => m.Member)
                .WithMany(m => m.Application)
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.Cascade);//刪掉後，相關聯的資料一併刪除
            });

            modelBuilder.Entity<Aifile>(options =>
            {
                options.HasOne(a => a.Application)
                .WithMany(a => a.Aifile)
                .HasForeignKey(a => a.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Embedding>(options =>
            {
                options.HasOne(d => d.Aifile)
                .WithMany(d => d.Embeddings)
                .HasForeignKey(d => d.AifileId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Chat>(options =>
            {
                options.HasOne(u => u.User)
                .WithMany(u => u.Chat)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Qahistory>(options =>
            {
                options.HasOne(c => c.Chat)
                .WithMany(c => c.Qahistorie)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, MemberEmail = "admin@gmail.com", MemberPassword = "670b14728ad9902aecba32e22fa4f6bd" }

            );

            modelBuilder.Entity<Application>().HasData(
                new Application { ApplicationId = 1, MemberId = 1, ApplicationName = "aaa" }
                );

            modelBuilder.Entity<Aifile>().HasData(
                new Aifile { AifileId = 1, AifileType = "json", AifilePath = "Upload/001.json", ApplicationId = 1 }
            );

            modelBuilder.Entity<Embedding>().HasData(
                new Embedding { EmbeddingId = 1, Qa = "abc", EmbeddingVector = "123,345,789", AifileId = 1 },
                new Embedding { EmbeddingId = 2, Qa = "abc", EmbeddingVector = "123,345,789", AifileId = 1 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserEmail = "admin@gmail.com", UserPassword = "670b14728ad9902aecba32e22fa4f6bd" }
               );

            modelBuilder.Entity<Chat>().HasData(
                new Chat { ChatId = 1, ChatTime = DateTime.Now, UserId = 1 }
            );

            modelBuilder.Entity<Qahistory>().HasData(
               new Qahistory { QahistoryId = 1, QahistoryQ = "Q", QahistoryA = "A", QahistoryVector = "123,456,778", ChatId = 1 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
