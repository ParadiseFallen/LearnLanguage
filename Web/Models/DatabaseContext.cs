using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Drawing;
using System.IO;

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

            modelBuilder
                .Entity<Language>()
                .Property(e => e.CultureInfo)
                .HasConversion(ci=>ci.Name,ci=>new CultureInfo(ci));
            modelBuilder
                .Entity<Language>()
                .Property(e => e.Flag)
                .HasConversion(x=>ImageToByte(x),x=> ByteToImage(x));
        }
        private static byte[] ImageToByte(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        private static Image ByteToImage(byte[] data)
        {
            using (MemoryStream mStream = new MemoryStream(data))
            {
                return Image.FromStream(mStream);
            }
        }
    }
}
