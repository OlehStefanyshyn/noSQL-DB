using BusinessLogic.Interface;
using MongoDTO;
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

namespace SocialWFApp
{
    public partial class UserList : Form
    {
        private readonly IUserManager _userManager;
        private readonly IAppUser _user;
        private List<AppUserModel> _Users;
        public UserList(IUserManager userManager,List<AppUserModel> Users, IAppUser user)
        {
            InitializeComponent();

            this._Users = Users;
            this._userManager = userManager;
            this._user = user;
            
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            var blUsers = new BindingList<AppUserModel>(_Users);

            bUsersSource.DataSource = blUsers;

            UsersNav.BindingSource = bUsersSource;
            UsersGrid.DataSource = bUsersSource;

        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var uPage = new UserPage(this._userManager, this._user, _Users[e.RowIndex].UserId);
            uPage.ShowDialog();
        }
    }
}
