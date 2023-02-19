namespace RadencyWebApi.DataAccess.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string? Cover { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    
    public Book(){}

    public Book(string title, string? cover, string content, string author, string genre)
    {
        Title = title;
        Cover = cover;
        Content = content;
        Author = author;
        Genre = genre;
    }
}