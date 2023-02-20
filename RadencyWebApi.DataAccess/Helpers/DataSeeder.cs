using RadencyWebApi.DataAccess.Entities;

namespace RadencyWebApi.DataAccess.Helpers;

public class DataSeeder
{
    private readonly DataContext _context;

    public DataSeeder(DataContext context) => _context = context;

    public async Task SeedAsync()
    {
        var books = new List<Book>
        {
            new Book
            {
                Title = "Adventures of Petro",
                Author = "Petro",
                Content = "That is book content",
                Genre = "Action"
            },
            new Book
            {
                Title = "Adventures of Ivan",
                Author = "Ivan",
                Content = "That is book content",
                Genre = "Drama"
            },
            new Book
            {
                Title = "Adventures of Vlad",
                Author = "Vlad",
                Content = "That is book content",
                Genre = "Western"
            },
            new Book
            {
                Title = "Adventures of Jason",
                Author = "Jason",
                Content = "That is book content",
                Genre = "Autobiography"
            },
            new Book
            {
                Title = "Adventures of Ruslan",
                Author = "Ruslan",
                Content = "That is book content",
                Genre = "Drama"
            },
            new Book
            {
                Title = "Adventures of Valera",
                Author = "Valera",
                Content = "That is book content",
                Genre = "Comedy"
            },
            new Book
            {
                Title = "Adventures of Artem",
                Author = "Artem",
                Content = "That is book content",
                Genre = "Comedy"
            },
            new Book
            {
                Title = "Adventures of James",
                Author = "James",
                Content = "That is book content",
                Genre = "Western"
            },
            new Book
            {
                Title = "Adventures of Andrii",
                Author = "Andrii",
                Content = "That is book content",
                Genre = "Drama"
            },
            new Book
            {
                Title = "Adventures of Jacob",
                Author = "Jacob",
                Content = "That is book content",
                Genre = "Action"
            }
        };

        await _context.AddRangeAsync(books);
        await _context.SaveChangesAsync();

        var ratings = new List<Rating>
        {
            new Rating
            {
                BookId = 1,
                Score = 5
            },
            new Rating
            {
                BookId = 1,
                Score = 2
            }
        };

        var reviews = new List<Review>
        {
            new Review
            {
                BookId = 2,
                Message = "weghwehg",
                Reviewer = "Jason"
            },
            new Review
            {
                BookId = 2,
                Message = "j4j4j",
                Reviewer = "Valera"
            },
            new Review
            {
                BookId = 1,
                Message = "233h3nn",
                Reviewer = "Alex"
            },
            new Review
            {
                BookId = 3,
                Message = "enjen",
                Reviewer = "Valera"
            },
            new Review
            {
                BookId = 4,
                Message = "hewhn",
                Reviewer = "Jason"
            }
        };
        
        await _context.AddRangeAsync(ratings);
        await _context.AddRangeAsync(reviews);
        await _context.SaveChangesAsync();
        
    }
}