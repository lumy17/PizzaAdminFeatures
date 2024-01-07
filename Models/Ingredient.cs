namespace PizzaApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }    
        public int Stock { get; set; }  

        //modelam relatia dintre ingredient-pizza: un ingredient poate sau nu sa fie inclus in una sa mai multe pizza
        public List<PizzaIngredient>? PizzaIngredients { get; set; } 
    }
}
