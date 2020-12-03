using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models.JsonConverters
{
    public class ImageConverter : JsonConverter<Image>
    {
        public override Image Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (MemoryStream mStream = new MemoryStream(reader.GetBytesFromBase64()))
            {
                return Image.FromStream(mStream);
            }
        }

        public override void Write(Utf8JsonWriter writer, Image img, JsonSerializerOptions options)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                writer.WriteBase64StringValue(mStream.ToArray());
            }
        }
    }
}
