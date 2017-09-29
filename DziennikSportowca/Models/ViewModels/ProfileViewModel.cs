using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            Invitations = new List<UserFriend>();
        }
        public List<UserFriend> Invitations { get; set; }
        public ApplicationUser User { get; set; }
    }
}
