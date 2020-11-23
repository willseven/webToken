
using System.Collections.Generic;
using ApiWeb_master.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb_master.Data
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");
    }


}