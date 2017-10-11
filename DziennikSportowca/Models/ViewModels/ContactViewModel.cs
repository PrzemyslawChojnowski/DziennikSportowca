using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class ContactViewModel
    {
        [Display(Name = "Twoje imię")]
        public string Name { get; set; }

        [Display(Name = "Twój adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Twój numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Wiadomość")]
        public string Message { get; set; }
    }
}
