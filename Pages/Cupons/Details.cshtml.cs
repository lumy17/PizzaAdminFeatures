using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Cupons
{
    public class DetailsModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public DetailsModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

      public Cupon Cupon { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cupon == null)
            {
                return NotFound();
            }

            var cupon = await _context.Cupon.FirstOrDefaultAsync(m => m.Id == id);
            if (cupon == null)
            {
                return NotFound();
            }
            else 
            {
                Cupon = cupon;
            }
            return Page();
        }
    }
}
