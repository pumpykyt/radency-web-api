using Microsoft.EntityFrameworkCore;
using RadencyWebApi.DataAccess.Entities;

namespace RadencyWebApi.DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    
    public DbSet<Book> Books { get; set; }
}