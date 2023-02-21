namespace RadencyWebApi.DataTransfer.Responses;

public class BookAbridgedResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public string Author { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
}