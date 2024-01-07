using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Cupons
{
    public class CreateModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public CreateModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cupon Cupon { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Cupon == null || Cupon == null)
            {
                return Page();
            }

            _context.Cupon.Add(Cupon);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
