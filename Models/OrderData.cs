namespace PizzaApp.Models
{
    //contine date legate de pizza, comenzi si relatiile dintre ele
    //transmitere/manipulare mai usoara in cadrul pag
    //pag pot sa lucreze cu o singura instanta a clasei pt a obtine toate inf 
    //necaesare despre pizza si comenzi.

    public class OrderData
    {
        public IEnumerable<Order> Orders { get; set; }  
        public IEnumerable<Pizza> Pizzas { get; set; }  
        public IEnumerable<PizzaOrder> PizzaOrders { get; set; }    
    }
}
