namespace RadencyWebApi.DataAccess.Entities;

public class Rating : BaseEntity
{
    public int BookId { get; set; }
    public int Score { get; set; }
    public virtual Book Book { get; set; }
}