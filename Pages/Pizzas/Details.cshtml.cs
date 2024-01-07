using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Pizzas
{
    public class DetailsModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public DetailsModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public Pizza Pizza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pizza == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.PizzaIngredients)
                .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            Pizza = pizza;
            return Page();
        }
    }
}
