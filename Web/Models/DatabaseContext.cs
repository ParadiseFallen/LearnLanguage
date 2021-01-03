using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Web.Models
{
    /// <summary>
    /// Database instance
    /// </summary>
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Language> Languages { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //var a = Phrases.Add(new Phrase() { Culture = new CultureInfo("en-EN"), Text = "fgbwahneuyignaweiujgouaw dfaw " });
            //var b = Phrases.Add(new Phrase() { Culture = new CultureInfo("ru-RU"), Text = "как так вышло " });
            //Translations.Add(new Translation() { A = a.Entity, B = b.Entity });
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        
    }
}
