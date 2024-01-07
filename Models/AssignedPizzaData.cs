namespace PizzaApp.Models
{
    //e specializata pt a gestiona inf necesare in cadrul paginilor. 
    //cod modular--portiuni ale codului sunt izolate si se ocupa de sarcini specifice.
    //e creata pt a gestiona inf referitoare la pizzele asignate in cadrul pag de edit..create

    //creata si pt a interactiona mai usor cu elem checkbox.
    //fiecare instanta a acestei clase corespunde unei categorii iar atributul
    //assigned poate controla starea checkboxului asociat categoriei
    //daca e bifat sau nu
    //gestioneaza datele pentru pizzele atribuite unei comenzi
    public class AssignedPizzaData
    {
        public int PizzaId { get; set; }    
        public string PizzaName { get; set; }
        public bool Assigned { get; set; }  
    }
}
