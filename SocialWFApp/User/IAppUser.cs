using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialWinFormApp.User
{
    public interface IAppUser
    {
        int UserId { get; set; }
        string Login { get; set; }
    }
}
