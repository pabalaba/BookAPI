using System.ComponentModel.DataAnnotations;

namespace BookAPI.Data.Entities
{
    public class PublishingHouse
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
