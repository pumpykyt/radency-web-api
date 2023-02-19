using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RadencyWebApi.DataAccess;
using RadencyWebApi.DataAccess.Entities;
using RadencyWebApi.DataTransfer.Responses;
using RadencyWebApi.Domain.Interfaces;

namespace RadencyWebApi.Domain.Services;

public class BookService : IBookService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<BookAbridgedResponse>> GetBooksAsync(string? order)
    {
        var query = _context.Books
            .Include(t => t.Reviews)
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

    public Task<List<Book>> GetTopTenBooksWithReviewsCountGreaterThanTen(string? genre)
    {
        throw new NotImplementedException();
    }
}