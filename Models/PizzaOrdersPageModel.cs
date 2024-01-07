using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaApp.Data;

namespace PizzaApp.Models
{

    //o clasa PageModel e creata pentru a gestiona logica specifica unei pagini web
    //si pt a raspunde la intercatiunile utilizatorului sau aalte even asociate cu acea pagina
    public class PizzaOrdersPageModel:PageModel
    {
        //creem o lista cu pizzele asociate unei comenzi
        public List<AssignedPizzaData> AssignedPizzaDataList { get; set; }

        public PizzaOrdersPageModel()
        {
            AssignedPizzaDataList = new List<AssignedPizzaData>();
        }
        //populam lista cu pizzele asociate comenzii
        //e folosita pentru a aduna datele necesare pt checkboxuri
        public void PopulateAssignedPizzaData(ApplicationDBContext context, Order order)
        {
            var allPizzas = context.Pizza;
            //pizzaOrders contine idurile pizzelor care sunt deja asociate cu comanda
            //curenta. Hashset e folosit pt eficienta in cautarea idurilor
            var pizzaOrders = new HashSet<int>(order.PizzaOrders.Select(p => p.PizzaId));

            //se parcurg pizzele si se creaza obiecte assignedpizzadata pt fiecare pizza
            //si marcheaza Assigned daca pizza e asoociata comenzii curente

            foreach (var pizza in allPizzas)
            {
                AssignedPizzaDataList.Add(new AssignedPizzaData
                {
                    PizzaId = pizza.Id,
                    PizzaName = pizza.PizzaName,
                    Assigned = pizzaOrders.Contains(pizza.Id)
                });
            }
        }

        //actualizeaza pizzele alese de utilizator. folosit in create si update
        public void UpdateOrdersPizza(ApplicationDBContext context, string[] selectedPizzas, Order orderToUpdate)
        {
            if (selectedPizzas == null)
            {
                orderToUpdate.PizzaOrders = new List<PizzaOrder>();
                return;
            }
            //selectedPizzas contine idurile pizzelor selectate de utilizator
            //se transofrma selectedPizzas in HashSet care e o tehnica de optimizare
            //hashset sunt eficiente pt operatiuni de cautare in special cand e 
            //vorba de verificare existentei unui element
            //idurile din selectedPizzas sunt obtinute din interfata utilizatorului unde
            //utilizatorul selecteaza pizzele pe care doreste sa le includa in comanda
            var selectedPizzasHS = new HashSet<string>(selectedPizzas);
            //hashset cu idurile pizzelor care sunt deja in orderToUpdate
            //adica asociate cu comanda pe care dorim sa o actualizam
            //id-urile din pizzaOrders aka ordersToUpdate provin din starea curenta a 
            //comenzii in sistem, reflectand ce pizze sunt in prezent asociate cu coamnda
            var pizzaOrders = new HashSet<int>(orderToUpdate.PizzaOrders.Select(p => p.Pizza.Id));

            foreach(var pizza in context.Pizza)
            {
                //verifica daca id pizzei curente din BD este in setul de pizze selectate
                if (selectedPizzasHS.Contains(pizza.Id.ToString()))
                {
                    //daca pizza curenta este selectata dar nu e in comanda..
                    if (!pizzaOrders.Contains(pizza.Id))
                    {
                        //se adauga pizza la comanda curenta
                        orderToUpdate.PizzaOrders.Add(
                            new PizzaOrder
                            {
                                OrderId = orderToUpdate.Id,
                                PizzaId = pizza.Id
                            });
                    }
                }
                else
                {
                    //verifica daca pizza curenta este in comanda curenta 
                    //(si nu ar trebui sa fie) e deselectata:
                    if (pizzaOrders.Contains(pizza.Id))
                    {
                        PizzaOrder courseToRemove = orderToUpdate.PizzaOrders
                            .SingleOrDefault(p => p.PizzaId == pizza.Id);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
