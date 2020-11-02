using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedModels.Models
{
    public class Translation
    {
        [Key]
        public int Id { get; set; }
        public int? AId { get; set; }
        [ForeignKey(nameof(AId))]
        public virtual Phrase A { get; set; }

        public int? BId { get; set; }
        [ForeignKey(nameof(BId))]
        public virtual Phrase B { get; set; }
    }
}
