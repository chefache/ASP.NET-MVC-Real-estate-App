﻿using Microsoft.EntityFrameworkCore;
using RealEstates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstates.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext()
        {

        }
        public RealEstateDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<RealEstateProperty> RealEstateProperties { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet <BuildingType> BuildingTypes { get; set; }

        public DbSet <PropertyType> PropertyTypes { get; set; }

        public DbSet <Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-AMLLGJ5;Database=RealEstate;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasMany(x => x.Properties)
                .WithOne(p => p.District)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite key
            modelBuilder.Entity<RealEstatePropertyTag>()
                .HasKey(x => new { x.PropertyId, x.TagId });
        }
    }
}