
using System.ComponentModel.DataAnnotations.Schema;

namespace kolos2.Models
{
    [Table("backpacks")]
    public class Backpacks
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }

        [ForeignKey("ItemId")]
        public Items Item { get; set; }
        
        [ForeignKey("CharacterId")]
        public Characters Character { get; set; }
    }
}
