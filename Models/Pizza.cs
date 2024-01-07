namespace PizzaApp.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public float BasePrice { get; set; }

        //asa se modeleaza faptul ca o pizza poate fi inclusa in mai multe comenzi(pizzaorders). 
        //in acest caz atributul este optional deoarece o pizza poate sa nu sa fie inclusa intr-o comanda
        public List<PizzaOrder>? PizzaOrders { get; set; }

        //modelam relatia dintre pizza si ingredient: o pizza poate sau nu sa contina unul sau mai multe ingrediente
        public List<PizzaIngredient>? PizzaIngredients { get; set; } 
    }
}
