using SocialWinFormApp.Models;
using SocialWinFormApp.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialWFmApp
{
    public partial class MyPage : Form
    {
        private readonly AppUserModel _user;
        private readonly IAppUser _appUser;
        public MyPage(AppUserModel user,IAppUser appUser)
        {
            this._user = user;
            this._appUser = appUser;
            InitializeComponent();
            UpdateData();
        }
        private void UpdateData()
        {
            Login.Text = this._appUser.Login;
            Name.Text = this._user.UserName;
            LastName.Text = this._user.UserLastName;
        }
    }
}
