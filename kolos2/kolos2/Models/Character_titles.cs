
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolos2.Models
{
    [Table("character_titles")]
    public class Character_titles
    {
        [Key, Column(Order = 0)]
        public int CharacterId { get; set; }

        [Key, Column(Order = 1)]
        public int TitleId { get; set; }

        public DateTime AcquireAt { get; set; }

        [ForeignKey("CharacterId")]
        public Characters Character { get; set; }
        
        [ForeignKey("TitleId")]
        public Titles Title { get; set; }
    }
}
