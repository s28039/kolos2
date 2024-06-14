
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kolos2.Models
{
    [Table("titles")]
    public class Titles
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Character_titles> CharacterTitlesCollection { get; set; }
    }
}
