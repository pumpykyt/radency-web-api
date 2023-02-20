using Microsoft.AspNetCore.Mvc;
using RadencyWebApi.DataTransfer.Requests;
using RadencyWebApi.Domain.Interfaces;

namespace RadencyWebApi.Presentation.Controllers;

[ApiController]
[Route("api/")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService) => _bookService = bookService;

    [HttpGet("books")]
    public async Task<IActionResult> GetBooks([FromQuery] string? order) 
        => Ok(await _bookService.GetBooksAsync(order));
    
    [HttpGet("recommended")]
    public async Task<IActionResult> GetTopTenBooksWithReviewsCountGreaterThanTen([FromQuery] string? genre) 
        => Ok(await _bookService.GetTopTenBooksWithReviewsCountGreaterThanTenAsync(genre));

    [HttpGet("books/{bookId}")]
    public async Task<IActionResult> GetBookById([FromRoute] int bookId, [FromQuery] string secret) 
        => Ok(await _bookService.GetBookWithDetailsAsync(bookId, secret));
    
    [HttpPost("books/save")]
    public async Task<IActionResult> CreateOrUpdateBook([FromBody] BookCreateRequest request) 
        => Ok(await _bookService.CreateBookAsync(request));

    [HttpPut("books/{bookId}/review")]
    public async Task<IActionResult> ReviewBookAsync([FromRoute] int bookId, [FromBody] ReviewCreateRequest request) 
        => Ok(await _bookService.CreateBookReviewAsync(request, bookId));
}