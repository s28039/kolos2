
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace kolos2.Models
{
    [Table("characters")]
    public class Characters
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(120)]
        public string LastName { get; set; }

        public int CurrentWei { get; set; }
        public int MaxWeight { get; set; }
        public ICollection<Backpacks> Backpacks { get; set; }
        public ICollection<Character_titles> CharacterTitles { get; set; }
    }
}
