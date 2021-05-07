using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionEmployee> InstitutionsEmployees { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PaymentHistory> PaymentHistorys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.AcceptedChats)
                .WithOne(c => c.Recipient)
                .HasForeignKey(c => c.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.InitiatedChats)
                .WithOne(c => c.Initiator)
                .HasForeignKey(c => c.InitiatorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.InstitutionEmployee)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.SendedMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Institution>()
                .HasMany(c => c.InstitutionEmployee)
                .WithOne(m => m.Institution)
                .HasForeignKey(m => m.InstitutionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Institution>()
                .HasOne(c => c.Wallet)
                .WithOne(m => m.Institution)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Institution>()
                .HasMany(c => c.Chats)
                .WithOne(m => m.Institution)
                .HasForeignKey(m => m.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wallet>()
                .HasMany(c => c.PaymentHistorys)
                .WithOne(m => m.Wallet)
                .HasForeignKey(m => m.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}