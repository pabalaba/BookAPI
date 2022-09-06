using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
