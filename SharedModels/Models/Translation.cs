using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class Translation
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("a")]
        public virtual Phrase A { get; set; }
        [JsonPropertyName("b")]
        public virtual Phrase B { get; set; }
        public override bool Equals(object other)
        {
            Translation otherTranslation = other as Translation;
            return
                (A.Text == otherTranslation.A.Text && B.Text == otherTranslation.B.Text) ||
                (B.Text == otherTranslation.A.Text && A.Text == otherTranslation.B.Text);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
