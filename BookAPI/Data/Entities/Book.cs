using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(12)]
        public string ISBN { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}