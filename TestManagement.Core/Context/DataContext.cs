using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.Models;

namespace TestManagement.Core.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<PcrTestBookings> PcrTestBookings { get; set; }
        public DbSet<PcrTestResults> PcrTestResults { get; set; }
        public DbSet<PcrTestVenueAllocations> PcrTestVenueAllocations { get; set; }
        public DbSet<PcrTestVenues> PcrTestVenues { get; set; }
    }
}
