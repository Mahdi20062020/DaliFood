using DaliFood.Models;
using DaliFood.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaliFood.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationCustomerUser>().HasBaseType<ApplicationUserDetail>();
            builder.Entity<ApplicationNormalUser>().HasBaseType<ApplicationUserDetail>();
            
            builder.Ignore<object>();
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategorie> ProductCategorie { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomersProduct> CustomersProduct { get; set; }
        public DbSet<PhotoFor> PhotoFor { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationUserDetail> ApplicationUserDetail { get; set; }
        public DbSet<PhoneNumbersToken> phoneNumbersTokens { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Deposit> Deposit { get; set; }
        public DbSet<Withdraw> Withdraw { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Providence> Providence { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CustomerComment> CustomerComment { get; set; }
    }
}

