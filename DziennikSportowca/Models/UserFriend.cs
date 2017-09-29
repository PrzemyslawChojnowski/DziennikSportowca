using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class UserFriend
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string FriendId { get; set; }
        [ForeignKey("FriendId")]
        public virtual ApplicationUser Friend { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }
    }

    public enum FriendshipStatus
    {
        InvitationSent = 1,

        Friends = 2
    };
}
