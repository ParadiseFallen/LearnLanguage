using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace Web.EntityConfiguration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                    .Property(e => e.CultureInfo)
                    .HasConversion(ci => ci.Name, ci => new CultureInfo(ci));
            builder
                    .Property(e => e.Flag)
                    .HasConversion(x => ImageToByte(x), x => ByteToImage(x));
        }
        private static byte[] ImageToByte(Image img)
        {
            using MemoryStream mStream = new MemoryStream();
            img.Save(mStream, img.RawFormat);
            return mStream.ToArray();
        }

        private static Image ByteToImage(byte[] data)
        {
            using MemoryStream mStream = new MemoryStream(data);
            return Image.FromStream(mStream);
        }
    }
}
