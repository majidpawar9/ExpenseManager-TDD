using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace ExpenseManager_TDD.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options):base(Options){

    }

    public DbSet<Transaction> Transactions {get; set;}
    public Dbset<Category> Categories {get; set;}
}
