using RadencyWebApi.DataAccess.Entities;

namespace RadencyWebApi.DataAccess.Helpers;

public class DataSeeder
{
    private readonly DataContext _context;

    public DataSeeder(DataContext context) => _context = context;

    public async Task Seed()
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
    }
}