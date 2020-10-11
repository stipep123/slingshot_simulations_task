using Microsoft.EntityFrameworkCore;
using SlingshotApplication.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SlingshotApplication.Context
{
    public class SlingshotContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Edge> Edges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (local);Database=SlingShotDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>().HasKey(n => n.Id);

            modelBuilder.Entity<Edge>().HasKey(e => e.Id);
        }
    }
}
