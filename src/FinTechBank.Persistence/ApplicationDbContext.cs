﻿using FinTechBank.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinTechBank.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Cliente { get; set; }
    }
}