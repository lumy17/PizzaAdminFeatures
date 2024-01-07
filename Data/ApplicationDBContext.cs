using Microsoft.EntityFrameworkCore;
using PizzaApp.Models;

namespace PizzaApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
                
        }
        public DbSet<Cupon> Cupon { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredient { get; set; }
        public DbSet<PizzaOrder> PizzaOrder { get; set; }
    }
}
