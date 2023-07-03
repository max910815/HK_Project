using HK_project.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HK_Product.Data
{
    public class HKContext : DbContext
    {
        public HKContext(DbContextOptions<HKContext> options) : base(options)
        {
            
        }
            public DbSet<Member> Member { get; set; }
            public DbSet<Application> Applications { get; set; }
            public DbSet<Chat> Chats { get; set; }
            public DbSet<Aifile> AIFiles { get; set; }
            public DbSet<Embedding> Embedding { get; set; }
            public DbSet<Qahistory> QAHistory { get; set; }
            public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(options =>
            {
                options.HasOne(m => m.Member).WithMany(m => m.Application).HasForeignKey(m => m.MemberId).OnDelete(DeleteBehavior.Cascade);//刪掉後，相關聯的資料一併刪除
            });

            modelBuilder.Entity<Aifile>(options =>
            {
                options.HasOne(a => a.Application).WithMany(a => a.Aifile).HasForeignKey(a => a.ApplicationId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Embedding>(options =>
            {
                options.HasOne(d => d.Aifile).WithMany(d => d.Embeddings).HasForeignKey(d => d.AifileId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Chat>(options =>
            {
                options.HasOne(u => u.User).WithMany(u => u.Chat).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Qahistory>(options =>
            {
                options.HasOne(c => c.Chat).WithMany(c => c.Qahistorie).HasForeignKey(c => c.ChatId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<User>(options =>
            {
                options.HasOne(c => c.Application).WithMany(c => c.User).HasForeignKey(c => c.ApplicationId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = "C0001", MemberName = "aa", MemberEmail = "aa@gmail.com", MemberPassword = "aaaaaa" }
                
            );

            modelBuilder.Entity<Application>().HasData(
                new Application { ApplicationId = "A0001", Model = null, Parameter = null, MemberId = "C0001" , ApplicationName = "aaa"}
                );

            modelBuilder.Entity<Aifile>().HasData(
                new Aifile { AifileId = "D0001", AifileType = "json", AifilePath = "Upload/001.json", ApplicationId = "A0001" , Language = "en" },
                new Aifile { AifileId = "D0002", AifileType = "json", AifilePath = "Upload/002.json", ApplicationId = "A0001" , Language = "en" }
            );



            modelBuilder.Entity<Embedding>().HasData(
                new Embedding { EmbeddingId = "E00001", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0001" },
                new Embedding { EmbeddingId = "E00002", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0001" },
                new Embedding { EmbeddingId = "E00003", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0002" },
                new Embedding { EmbeddingId = "E00004", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0002" }

            );

            modelBuilder.Entity<Chat>().HasData(
                new Chat { ChatId = "C0001", ChatTime = DateTime.Now, ChatName = "Gay", UserId = "U0001" }
            );

            modelBuilder.Entity<Qahistory>().HasData(
               new Qahistory { QahistoryId = "Q0001", QahistoryQ = "abc", QahistoryA = "abc", QahistoryVectors = "123,456,778", ChatId = "C0001" },
               new Qahistory { QahistoryId = "Q0002", QahistoryQ = "abc", QahistoryA = "abc", QahistoryVectors = "123,456,789", ChatId = "C0001" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = "U0001", UserName = "aaa", UserEmail = "aaa@gmail.com", UserPassword = "aaaaaa",ApplicationId = "A0001" }
               );



            base.OnModelCreating(modelBuilder);
        }
    }
}
