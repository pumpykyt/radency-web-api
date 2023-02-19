using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Responses;

namespace RadencyWebApi.Domain.Interfaces;

public interface IBookService
{
    Task<List<BookAbridgedResponse>> GetBooksAsync(string? order);
    Task<List<Book>> GetTopTenBooksWithReviewsCountGreaterThanTen(string? genre);
}