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
    public class IndexModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public IndexModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Cupon> Cupon { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cupon != null)
            {
                Cupon = await _context.Cupon.ToListAsync();
            }
        }
    }
}
