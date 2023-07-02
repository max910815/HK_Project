using HK_Product.Models;
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
            //modelBuilder.Entity<Application>(options =>
            //{
            //    options.HasOne(m => m.Member).WithMany(m => m.Applications).HasForeignKey(m => m.MemberId).OnDelete(DeleteBehavior.Cascade);//刪掉後，相關聯的資料一併刪除
            //});

            modelBuilder.Entity<Aifile>(options =>
            {
                options.HasOne(a => a.Application).WithMany(a => a.Aifiles).HasForeignKey(a => a.ApplicationId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Embedding>(options =>
            {
                options.HasOne(d => d.Aifile).WithMany(d => d.Embeddings).HasForeignKey(d => d.AifileId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Chat>(options =>
            {
                options.HasOne(u => u.User).WithMany(u => u.Chats).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Qahistory>(options =>
            {
                options.HasOne(c => c.Chat).WithMany(c => c.Qahistories).HasForeignKey(c => c.ChatId).OnDelete(DeleteBehavior.Cascade);
            });


            //modelBuilder.Entity<Member>().HasData(
            //    new Member { MemberId = "C0001", MemberName = "Althea", MemberEmail = "althea@gmail.com", MemberPassword = "althea01" },
            //    new Member { MemberId = "C0002", MemberName = "Jimmy", MemberEmail = "jimmy@gmail.com", MemberPassword = "jimmy02" }
            //);

            modelBuilder.Entity<Application>().HasData(
                new Application { ApplicationId = "A0001", Model = null, Parameter = null, UserId = "U0001" }
                );

            modelBuilder.Entity<Aifile>().HasData(
                new Aifile { AifileId = "D0001", AifileType = "json", AifilePath = "Upload/001.json", ApplicationId = "A0001" },
                new Aifile { AifileId = "D0002", AifileType = "json", AifilePath = "Upload/002.json", ApplicationId = "A0001" }
            );



            modelBuilder.Entity<Embedding>().HasData(
                new Embedding { EmbeddingId = "E0001", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0001" },
                new Embedding { EmbeddingId = "E0002", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0001" },
                new Embedding { EmbeddingId = "E0003", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0002" },
                new Embedding { EmbeddingId = "E0004", EmbeddingQuestion =null, EmbeddingAnswer =null, Qa = "abc", EmbeddingVectors = "123,345,789", AifileId = "D0002" }

            );

            modelBuilder.Entity<Chat>().HasData(
                new Chat { ChatId = "C0001", ChatTime = new DateTime(2023 / 05 / 30), ChatName = "Gay", UserId = "U0001" }
            );

            modelBuilder.Entity<Qahistory>().HasData(
               new Qahistory { QahistoryId = "Q0001", QahistoryQ = "abc", QahistoryA = "abc", QahistoryVectors = "123,456,778", ChatId = "C0001" },
               new Qahistory { QahistoryId = "Q0002", QahistoryQ = "abc", QahistoryA = "abc", QahistoryVectors = "123,456,789", ChatId = "C0001" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = "U0001", UserName = "aaa", UserEmail = "aaa@gmail.com", UserPassword = "aaaaaa"}
               );



            base.OnModelCreating(modelBuilder);
        }
    }
}
