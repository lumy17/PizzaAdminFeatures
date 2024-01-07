using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Models;

namespace PizzaApp.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly PizzaApp.Data.ApplicationDBContext _context;

        public IndexModel(PizzaApp.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public MemberIndexData MemberData { get; set; }
        public int MemberId { get; set; }
        public int OrderId { get; set; }

        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }    

        public async Task OnGetAsync(int? id, int? orderId, string sortOrder)
        {
            if (_context.Member != null)
            {
                Member = await _context.Member.ToListAsync();
            }

            FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";

            MemberData = new MemberIndexData();
            MemberData.Member = await _context.Member
                .Include(i => i.Orders)
                .OrderBy(i => i.FirstName)
                .ToListAsync();

            if (id != null)
            {
                MemberId = id.Value;
                Member member = MemberData.Member
                    .Where(i => i.Id == id.Value).Single();
                MemberData.Orders = member.Orders;
            }
            switch(sortOrder)
            {
                case "firstname_desc":
                    MemberData.Member = MemberData.Member.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname_desc":
                    MemberData.Member = MemberData.Member.OrderByDescending(s => s.LastName);
                    break;
                default:
                    MemberData.Member = MemberData.Member.OrderBy(s => s.FirstName);
                    break;
            }
        }
    }
}
