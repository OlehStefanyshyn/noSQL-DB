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

namespace SocialWFApp
{
    public partial class AddPost : Form
    {
        private readonly IPostManager _postManager;
        private readonly IAppUser _user;
        public AddPost(IPostManager postManager,IAppUser user)
        {
            this._postManager = postManager;
            this._user = user;
            InitializeComponent();
        }

        private void PublishButton_Click(object sender, EventArgs e)
        {
            this._postManager.CreatePost(this._user.UserId, Titte.Text, Body.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
