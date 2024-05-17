﻿using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tbl_Account> Tbl_Account { get; set; }
        public DbSet<Tbl_State> Tbl_State { get; set; }
        public DbSet<Tbl_Township> Tbl_Township { get; set; }
    }
}