using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Pizzas
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public EditModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pizza Pizza { get; set; }

        [BindProperty]
        public int[] SelectedIngredients { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pizza == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.PizzaIngredients)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }

            Pizza = pizza;

            var allIngredients = await _context.Ingredient.ToListAsync();
            ViewData["Ingredients"] = new SelectList(allIngredients, "Id", "Name");
            SelectedIngredients = pizza.PizzaIngredients.Select(pi => pi.IngredientId).ToArray();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var pizzaToUpdate = await _context.Pizza
                .Include(p => p.PizzaIngredients)
                .FirstOrDefaultAsync(p => p.Id == Pizza.Id);

            if (pizzaToUpdate == null)
            {
                return NotFound();
            }

            pizzaToUpdate.PizzaName = Pizza.PizzaName;
            pizzaToUpdate.BasePrice = Pizza.BasePrice;
            pizzaToUpdate.ImageUrl = Pizza.ImageUrl;

            // Actualizează lista de ingrediente
            _context.PizzaIngredient.RemoveRange(pizzaToUpdate.PizzaIngredients);

            if (SelectedIngredients != null)
            {
                foreach (var ingredientId in SelectedIngredients)
                {
                    pizzaToUpdate.PizzaIngredients.Add(new PizzaIngredient
                    {
                        IngredientId = ingredientId,
                        PizzaId = pizzaToUpdate.Id
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(Pizza.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.Id == id);
        }
    }
}
