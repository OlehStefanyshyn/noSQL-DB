using BusinessLogic.Interface;
using MongoDTO;
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

namespace SocialWFApp
{
    public partial class RegisterForm : Form
    {
        private readonly IUserManager _userManager;
        private readonly IAppUser _user;
        public RegisterForm(IUserManager userManager,IAppUser user)
        {
            this._userManager = userManager;
            this._user = user;
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var add = new UserDTO
            {
                UserLogin = Login.Text,
                UserName = Name.Text,
                Email = Email.Text,
                Interests = Interests.Text.Split(',').ToList(),
                UserLastName = LastName.Text,
                UserPassword = Password.Text
            };
            this._userManager.CreateUser(add);
            this.Close();

        }
    }
}
