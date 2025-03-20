using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.Entities;

namespace MyPlayListApp.Data.Context
{
    public class MyPlayListAppContext : DbContext
    {
        public MyPlayListAppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Song>()
          .HasOne(s => s.Singer)
          .WithMany(s => s.Songs)
          .HasForeignKey(s => s.SingerId)
          .IsRequired();

            modelBuilder.Entity<Song>()
           .HasOne(s => s.Category)
           .WithMany(c => c.Songs)
           .HasForeignKey(s => s.CategoryId)
           .IsRequired();

            var category1 = new Category { Id = Guid.NewGuid(), Name = "Tragedy" };
            var category2 = new Category { Id = Guid.NewGuid(), Name = "Rock"};
            var category3 = new Category { Id = Guid.NewGuid(), Name = "Romance" };

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);

            var singer1 = new Singer { Id = Guid.NewGuid(), Name = "Tamer Hosny", DateOfBirth = new DateOnly(1977, 8, 11), Image = "TamerHosny.jpg" };
            var singer2 = new Singer { Id = Guid.NewGuid(), Name = "Mohamed Hamaki", DateOfBirth = new DateOnly(1975, 11, 4), Image = "MohamedHamaki.jpg" };
            var singer3 = new Singer { Id = Guid.NewGuid(), Name = "Sherine Abdel Wahab", DateOfBirth = new DateOnly(1980, 10, 10), Image = "SherineAbdelWahab.jpg" };

            modelBuilder.Entity<Singer>().HasData(singer1, singer2, singer3);

            modelBuilder.Entity<Song>().HasData(
                new Song { Id = Guid.NewGuid(), Name = "Naseeny Leeh", SingerId = singer1.Id, CategoryId = category1.Id },
                new Song { Id = Guid.NewGuid(), Name = "Bahebak Kol Youm", SingerId = singer2.Id, CategoryId = category2.Id },
                new Song { Id = Guid.NewGuid(), Name = "Mashaer", SingerId = singer3.Id, CategoryId = category3.Id }
            );

        }

        public DbSet<Singer> Singers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
