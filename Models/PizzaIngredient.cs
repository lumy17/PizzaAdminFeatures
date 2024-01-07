namespace PizzaApp.Models
{
    public class PizzaIngredient
    {
        //EF requires for you to have a primary key.
        public int Id { get; set; }
        //fk catre pizzza
        public int PizzaId { get; set; } 
        //navigation property
        public Pizza Pizza { get; set; }
        public int IngredientId { get; set; }   
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
