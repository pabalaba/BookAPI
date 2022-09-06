using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class AuthorModel : AuthorBaseModel
    {
        public int Id { get; set; }
    }
}
