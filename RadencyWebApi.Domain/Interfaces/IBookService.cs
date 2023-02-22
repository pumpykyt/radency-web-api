using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Requests;
using RadencyWebApi.DataTransfer.Responses;

namespace RadencyWebApi.Domain.Interfaces;

public interface IBookService
{
    Task<List<BookAbridgedResponse>> GetBooksAsync(string? order);
    Task<List<BookAbridgedResponse>> GetTopTenBooksWithReviewsCountGreaterThanTenAsync(string? genre);
    Task<BookDetailedResponse> GetBookWithDetailsAsync(int bookId);
    Task<BookCreateResponse> CreateBookAsync(BookCreateRequest request);
    Task<ReviewCreateResponse> CreateBookReviewAsync(ReviewCreateRequest request, int bookId);
    Task<bool> DeleteBookAsync(int bookId, string secret);
    Task<RatingResponse> RateBookAsync(RatingCreateRequest request, int bookId);
}