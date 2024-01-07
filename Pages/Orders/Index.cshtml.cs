using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public IndexModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;
        public OrderData OrderD { get; set; }
        public int OrderId { get; set; }    
        public int PizzaId { get; set; }

        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? pizzaId, string searchString)
        {
            OrderD = new OrderData();

            //primeste numele clientului ca parametru si cauta comenzi in functie de acesta
            CurrentFilter = searchString;

            OrderD.Orders = await _context.Order
                .Include(o => o.Cupon)
                .Include(o => o.Member)
                .Include(o => o.PizzaOrders)
                .ThenInclude(o => o.Pizza)
                .ThenInclude(o => o.PizzaIngredients)
                .ThenInclude(o=>o.Ingredient)
                .AsNoTracking()
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                OrderD.Orders = OrderD.Orders.Where(
                    s => s.Member.FullName.Contains(searchString));
            }

            if (id != null)
            {
                OrderId = id.Value;
                Order order = OrderD.Orders
                    .Where(i => i.Id == id.Value).Single();
                OrderD.Pizzas = order.PizzaOrders.Select(s => s.Pizza);
            }
        }
    }
}
