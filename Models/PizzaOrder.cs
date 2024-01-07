namespace PizzaApp.Models
{
    public class PizzaOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        //proprietate de navigare -- reprezinta o relatie cu alta clasa: in acest caz repr relatia cu clasa Order
        public Order Order { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }    
        public float FinalPrice { get; set; }
    }
}
