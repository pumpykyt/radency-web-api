using Mapster;
using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Requests;
using RadencyWebApi.DataTransfer.Responses;

namespace RadencyWebApi.Domain.Mapping;

public class MappingRegistration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookAbridgedResponse>()
            .Map(dest => dest.Rating, src => src.Ratings.Count > 0 ? (decimal)src.Ratings.Average(t => t.Score) : 0)
            .Map(dest => dest.ReviewsCount, src => src.Reviews.Count);

        config.NewConfig<Book, BookDetailedResponse>()
            .Map(dest => dest.Rating, src => src.Ratings.Count > 0 ? (decimal)src.Ratings.Average(t => t.Score) : 0)
            .Map(dest => dest.ReviewsCount, src => src.Reviews.Count)
            .Map(dest => dest.Reviews, src => src.Reviews.ToList());

        config.NewConfig<BookCreateRequest, Book>();
        config.NewConfig<ReviewCreateRequest, Review>();
    }
}