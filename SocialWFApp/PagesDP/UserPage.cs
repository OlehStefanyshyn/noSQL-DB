using BusinessLogic.Interface;
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
    public partial class UserPage : Form
    {
        private readonly IUserManager _userManager;
        private readonly IAppUser _user;
        private readonly int _ChosenId;
        public UserPage(IUserManager userManager,IAppUser user, int ChosenId)
        {
            this._ChosenId = ChosenId;
            this._userManager = userManager;
            this._user = user;
            InitializeComponent();
            UpdateData();
        }
        private void UpdateData()
        {
            var chosen_user = this._userManager.GetUserById(this._ChosenId);
            UName.Text = chosen_user.UserName;
            ULName.Text = chosen_user.UserLastName;
            Login.Text = chosen_user.UserLogin;
            FollowUnfollowButton.Text = this._userManager.IsFollowed(this._user.UserId, this._ChosenId) ? "Unfollow":"Follow";
            PathLenLable.Text = Convert.ToString(this._userManager.MinPathBetween(this._user.UserId, this._ChosenId));
        }

        private void FollowButton_Click(object sender, EventArgs e)
        {
            if (this._userManager.IsFollowed(this._user.UserId, this._ChosenId))
            {
                this._userManager.UnFollow(this._user.UserId, this._ChosenId);
            }
            else
            {
                this._userManager.Follow(this._user.UserId, this._ChosenId);
            }
            
            UpdateData();
        }
    }
}
