using Microsoft.EntityFrameworkCore;
using PizzaProjeto.Models;

namespace PizzaProjeto.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<PizzaModel> Pizzas { get; set; }
}