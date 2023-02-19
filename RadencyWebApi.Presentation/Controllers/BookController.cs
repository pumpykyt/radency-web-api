using Microsoft.AspNetCore.Mvc;
using RadencyWebApi.Domain.Interfaces;

namespace RadencyWebApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService) => _bookService = bookService;

    [HttpGet]
    public async Task<IActionResult> GetBooksAsync([FromQuery] string? order) 
        => Ok(await _bookService.GetBooksAsync(order));
}