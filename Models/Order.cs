namespace PizzaApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public int Number { get; set; } 
        public int? Apartment {  get; set; }
        public string Status { get; set; }

        //asa se modeleaza faptul ca o comanda poate avea mai multe pizza(pizzaorder) asociate. Este obligatoriu ca o comanda sa aiba o pizza asociata
        public List<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();
        //fk catre Cupon
        public int? CuponId { get; set; }    
        //navigation prop
        public Cupon? Cupon { get; set; }
        //fk catre Member, e oblig relatia
        public int MemberId { get; set; }

        //modelstate is invalid daca nu initializez!!!!
        public Member Member { get; set; } = new Member();

        public float CalculateFinalPrice()
        {
            float finalPrice = 0;
            foreach(var pizzaOrder in PizzaOrders)
            {
                finalPrice += pizzaOrder.Pizza.BasePrice;
                if(pizzaOrder.Pizza.PizzaIngredients != null)
                {
                    foreach(var ingredient in pizzaOrder.Pizza.PizzaIngredients)
                    {
                        finalPrice += ingredient.Ingredient.Price;
                    }
                }
            }
            if (Cupon != null && Cupon.StartDate <= DateTime.Now && Cupon.EndDate >= DateTime.Now)
            {
                finalPrice = finalPrice - Cupon.Value * finalPrice;
            }

            return finalPrice;
        }
    }
}
