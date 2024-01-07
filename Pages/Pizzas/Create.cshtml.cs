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
    public class CreateModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public CreateModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pizza Pizza { get; set; }
        [BindProperty]
        public int[] SelectedIngredients { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Ingredients = await _context.Ingredient.ToListAsync();
            ViewData["ShowIngredients"] = new SelectList(Ingredients, "Id", "Name");
       
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Pizza == null || Pizza == null)
            {
                return Page();
            }
            _context.Pizza.Add(Pizza);
            await _context.SaveChangesAsync();
            if (SelectedIngredients != null && SelectedIngredients.Length > 0)
            {
                foreach (var ingredientId in SelectedIngredients)
                {
                     var newPizzaIngredient = new PizzaIngredient
                    {
                        PizzaId = Pizza.Id,
                        IngredientId = ingredientId,
                        Quantity = 1
                    };
                    _context.PizzaIngredient.Add(newPizzaIngredient);
                }
            }
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}