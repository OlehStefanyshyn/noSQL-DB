using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialWinFormApp.User
{
    public class AppUser : IAppUser
    {
        public int UserId { get; set; }
        public string Login { get; set; }
    }
}
