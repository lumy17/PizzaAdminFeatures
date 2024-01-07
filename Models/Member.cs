using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$",
            ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula" + "(ex.Ana sau Ana Maria sau Ana-Maria")]

        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [StringLength(70)]
        public string Email { get; set; } = string.Empty;

        [RegularExpression("^0([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
            ErrorMessage = "Telefonul trebuie sa fie de forma'0722-112-123' sau '0722.122.123' sau '0722 123 123'")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //modelam relatia cu orders: un membru poate sa faca una sau mai multe comenzi
        //nu e neaparat ca un client sa faca o comanda
        public List<Order>? Orders { get; set; }
    }
}
