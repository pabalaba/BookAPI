using System;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class AuthorBaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
