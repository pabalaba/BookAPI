using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class BookInsertModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(12)]
        public string ISBN { get; set; }
    }
}
