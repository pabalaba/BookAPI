using System.Collections.Generic;

namespace BookAPI.Models
{
    public class BookModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public PublishingHouseModel PublishingHouse { get; set; }
        public ICollection<AuthorModel> Authors { get; set; }
    }
}
