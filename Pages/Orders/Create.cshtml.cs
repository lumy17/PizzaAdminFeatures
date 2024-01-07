using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Orders
{
    public class CreateModel : PizzaOrdersPageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public CreateModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CuponId"] = new SelectList(_context.Cupon, "Id", "Code");
        ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");

            var order = new Order();

            order.PizzaOrders = new List<PizzaOrder>();

            PopulateAssignedPizzaData(_context, order);
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedPizzas)
        {
            var newOrder = new Order();

            if (selectedPizzas != null)
            {
                newOrder.PizzaOrders = new List<PizzaOrder>();
                foreach (var pizza in selectedPizzas)
                {
                    var pizzaToAdd = new PizzaOrder
                    {
                        PizzaId = int.Parse(pizza)
                    };
                    newOrder.PizzaOrders.Add(pizzaToAdd);
                }
            }
            Order.PizzaOrders = newOrder.PizzaOrders;
            _context.Order.Add(Order);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
