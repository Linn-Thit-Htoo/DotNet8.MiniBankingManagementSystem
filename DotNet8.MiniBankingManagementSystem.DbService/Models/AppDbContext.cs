using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.MiniBankingManagementSystem.DbService.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Township> Townships { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<Withdraw> Withdraws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountLevel).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.ToTable("Deposit");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.DepositDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.StateId).ValueGeneratedNever();
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.StateName).HasMaxLength(50);
        });

        modelBuilder.Entity<Township>(entity =>
        {
            entity.ToTable("Township");

            entity.Property(e => e.TownshipId).ValueGeneratedNever();
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
            entity.Property(e => e.TownshipName).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.ToTable("TransactionHistory");

            entity.Property(e => e.FromAccountNo).HasMaxLength(50);
            entity.Property(e => e.ToAccountNo).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Withdraw>(entity =>
        {
            entity.HasKey(e => e.WithDrawId).HasName("PK_WithDraw");

            entity.ToTable("Withdraw");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.WithDrawDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
