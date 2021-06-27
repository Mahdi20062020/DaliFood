using DaliFood.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaliFood.AdminPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategorie> ProductCategorie { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}

