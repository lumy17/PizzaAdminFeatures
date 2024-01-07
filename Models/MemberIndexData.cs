namespace PizzaApp.Models
{
    public class MemberIndexData
    {
        public IEnumerable<Member> Member { get; set; } 
        public IEnumerable<Order> Orders { get; set; }
    }
}
