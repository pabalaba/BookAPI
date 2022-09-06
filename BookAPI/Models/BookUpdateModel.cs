using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class BookUpdateModel
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
    }
}
