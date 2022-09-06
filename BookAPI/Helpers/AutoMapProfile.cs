using AutoMapper;
using BookAPI.Data.Entities;
using BookAPI.Models;

namespace BookAPI.Helpers
{
    public class AutoMapProfile: Profile
    {
        public AutoMapProfile()
        {
            CreateMap<BookModel, Book>().ReverseMap();
            CreateMap<BookInsertModel, Book>();
            CreateMap<BookUpdateModel, Book>();
            CreateMap<AuthorModel, Author>().ReverseMap();
            CreateMap<AuthorBaseModel, AuthorModel>().ReverseMap();
            CreateMap<AuthorBaseModel, Author>().ReverseMap();
            CreateMap<PublishingHouseBaseModel, PublishingHouseModel>().ReverseMap();
            CreateMap<PublishingHouseModel, PublishingHouse>().ReverseMap();
            CreateMap<PublishingHouseBaseModel, PublishingHouse>().ReverseMap();
            //TODO Implementare Mapping mancanti
        }
    }
}
