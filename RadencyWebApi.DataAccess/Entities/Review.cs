namespace RadencyWebApi.DataAccess.Entities;

public class Review : BaseEntity
{
    public string Message { get; set; }
    public int BookId { get; set; }
    public string Reviewer { get; set; }
    public virtual Book Book { get; set; }
}