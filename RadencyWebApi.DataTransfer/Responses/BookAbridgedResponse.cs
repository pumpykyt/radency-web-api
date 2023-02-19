namespace RadencyWebApi.DataTransfer.Responses;

public class BookAbridgedResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
}