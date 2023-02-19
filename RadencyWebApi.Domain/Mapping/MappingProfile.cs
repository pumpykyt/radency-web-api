using AutoMapper;
using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Responses;

namespace RadencyWebApi.Domain.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookAbridgedResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => (decimal)src.Ratings.Average(t => t.Score)))
            .ForMember(dest => dest.ReviewsCount, opt => opt.MapFrom(src => src.Reviews.Count));
    }
}