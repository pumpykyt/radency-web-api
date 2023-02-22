using System.Net;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RadencyWebApi.DataAccess;
using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Requests;
using RadencyWebApi.DataTransfer.Responses;
using RadencyWebApi.Domain.Configs;
using RadencyWebApi.Domain.Exceptions;
using RadencyWebApi.Domain.Interfaces;

namespace RadencyWebApi.Domain.Services;

public class BookService : IBookService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly SecretPhraseConfig _secretPhraseConfig;

    public BookService(DataContext context, IMapper mapper, IOptions<SecretPhraseConfig> options)
    {
        _context = context;
        _mapper = mapper;
        _secretPhraseConfig = options.Value;
    }
    
    public async Task<List<BookAbridgedResponse>> GetBooksAsync(string? order)
    {
        var query = _context.Books
            .Include(t => t.Reviews)
            .Include(t => t.Ratings)
            .AsNoTracking();
        
        if (string.IsNullOrWhiteSpace(order))
        {
            return _mapper.Map<List<Book>, List<BookAbridgedResponse>>(await query.ToListAsync());
        }

        switch (order)
        {
            case "author":
                query = query.OrderBy(t => t.Author);
                break;
            case "title":
                query = query.OrderBy(t => t.Title);
                break;
        }
        
        return _mapper.Map<List<Book>, List<BookAbridgedResponse>>(await query.ToListAsync());
    }

    public async Task<List<BookAbridgedResponse>> GetTopTenBooksWithReviewsCountGreaterThanTenAsync(string? genre)
    {
        var query = _context.Books
            .Include(t => t.Reviews)
            .Include(t => t.Ratings)
            .AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(t => t.Genre == genre);
        }

        query = query.Where(t => t.Reviews.Count > 10)
                     .OrderByDescending(t => t.Ratings.Count > 0 ? t.Ratings.Average(x => x.Score) : 0)
                     .Take(10)
                     .OrderByDescending(t => t.Ratings.Count > 0 ? t.Ratings.Average(x => x.Score) : 0);
        
        return _mapper.Map<List<Book>, List<BookAbridgedResponse>>(await query.ToListAsync());
    }

    public async Task<BookDetailedResponse> GetBookWithDetailsAsync(int bookId)
    {
        var book = await _context.Books.AsNoTracking()
                                       .Include(t => t.Reviews)
                                       .Include(t => t.Ratings)
                                       .SingleOrDefaultAsync(t => t.Id == bookId);

        if (book is null)
        {
            throw new HttpException(HttpStatusCode.NotFound);
        }

        return _mapper.Map<Book, BookDetailedResponse>(book);
    }

    public async Task<BookCreateResponse> CreateBookAsync(BookCreateRequest request)
    {
        var newEntity = _mapper.Map<BookCreateRequest, Book>(request);
        
        if (request.Id is null)
        {
            await _context.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            
            return new BookCreateResponse { Id = newEntity.Id };
        }

        var bookExists = await _context.Books.AsNoTracking()
                                             .AnyAsync(t => t.Id == request.Id);
        if (!bookExists)
        {
            throw new HttpException(HttpStatusCode.NotFound);
        }

        _context.Update(newEntity);
        await _context.SaveChangesAsync();

        return new BookCreateResponse { Id = newEntity.Id };
    }

    public async Task<ReviewCreateResponse> CreateBookReviewAsync(ReviewCreateRequest request, int bookId)
    {
        var bookExists = await _context.Books.AsNoTracking()
                                             .AnyAsync(t => t.Id == bookId);
        if (!bookExists)
        {
            throw new HttpException(HttpStatusCode.NotFound);
        }
        
        var entity = _mapper.Map<ReviewCreateRequest, Review>(request);
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return new ReviewCreateResponse { Id = entity.Id };
    }

    public async Task<bool> DeleteBookAsync(int bookId, string secret)
    {
        if (secret != _secretPhraseConfig.Secret)
        {
            throw new HttpException(HttpStatusCode.Unauthorized);
        }

        var book = await _context.Books.SingleOrDefaultAsync(t => t.Id == bookId);
        if (book is null)
        {
            throw new HttpException(HttpStatusCode.NotFound);
        }

        _context.Remove(book);
        
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<RatingResponse> RateBookAsync(RatingCreateRequest request, int bookId)
    {
        var bookExists = await _context.Books.AsNoTracking()
                                             .AnyAsync(t => t.Id == bookId);
        
        if (!bookExists)
        {
            throw new HttpException(HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<RatingCreateRequest, Rating>(request);
        entity.BookId = bookId;

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new RatingResponse { Score = entity.Score };
    }
}