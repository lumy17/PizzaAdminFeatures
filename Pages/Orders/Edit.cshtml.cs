using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Orders
{
    public class EditModel : PizzaOrdersPageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public EditModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order =  await _context.Order
                .Include(o => o.Cupon)
                .Include(o => o.Member)
                .Include(o => o.PizzaOrders).ThenInclude(p => p.Pizza)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            //apelam populateassignedpizzadata pt a obtine inf necesare checboxurilor
            //folosind clasa assigned pizzadata
            PopulateAssignedPizzaData(_context, Order);

           ViewData["CuponId"] = new SelectList(_context.Cupon, "Id", "Code", Order.CuponId);
           ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName", Order.MemberId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedPizzas)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Order
                .Include(o => o.Cupon)
                .Include(o => o.Member)
                .Include(o => o.PizzaOrders)
                .ThenInclude(o => o.Pizza)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (orderToUpdate == null)
            {
                return NotFound();
            } 
            if (await TryUpdateModelAsync<Order>(
                orderToUpdate, "Order", o => o.Date, o => o.Street, o => o.Number,
                o => o.Apartment, o => o.Status, o => o.CuponId, o => o.MemberId))
            {
                UpdateOrdersPizza(_context, selectedPizzas, orderToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateOrdersPizza(_context, selectedPizzas, orderToUpdate);
            PopulateAssignedPizzaData(_context, orderToUpdate);
     
            return Page();
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
