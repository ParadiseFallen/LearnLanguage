﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels.Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Web.Models
{
    /// <summary>
    /// Database instance
    /// </summary>
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Translation> Translations { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            //var a = Phrases.Add(new Phrase() { Culture = new CultureInfo("en-EN"), Text = "fgbwahneuyignaweiujgouaw dfaw " });
            //var b = Phrases.Add(new Phrase() { Culture = new CultureInfo("ru-RU"), Text = "как так вышло " });
            //Translations.Add(new Translation() { A = a.Entity, B = b.Entity });
            this.SaveChanges();
        }
        public Phrase GetRandomPhrase(CultureInfo cultureInfo) => 
            Phrases
                .Where(p=>p.Culture==cultureInfo)
                .Skip(new Random().Next(0, Phrases.Count(p=>p.Culture == cultureInfo)))
                .Take(1)
                .FirstOrDefault();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Phrase>()
                .Property(e => e.Culture)
                .HasConversion(ci=>ci.Name,ci=>new CultureInfo(ci));
            base.OnModelCreating(modelBuilder);
        }
    }
}
