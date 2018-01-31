using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.AccountViewModels
{
    public class EditProfileViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Aktualne imię")]
        public string ActualUserName { get; set; }
        [Display(Name = "Aktualne nazwisko")]
        public string ActualUserSurname { get; set; }
        [Display(Name = "Płeć")]
        public string ActualUserGender { get; set; }
        [Display(Name = "Aktualne zdjęcie profilowe")]
        public byte[] ActualUserProfilePicture { get; set; }

        [Display(Name = "Nowe imię")]
        public string NewUserName { get; set; }
        [Display(Name = "Nowe nazwisko")]
        public string NewUserSurname { get; set; }
        [Display(Name = "Płeć")]
        public Gender NewUserGender { get; set; }
        [Display(Name = "Nowe zdjęcie profilowe")]
        public IFormFile NewProfilePicture { get; set; }
    }
}
