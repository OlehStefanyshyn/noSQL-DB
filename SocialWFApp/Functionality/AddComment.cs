using BusinessLogic.Interface;
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
    public partial class AddComment : Form
    {
        private readonly IPostManager _postManager;
        private readonly IAppUser _user;
        private readonly AppPostModel _post;
        public AddComment(IPostManager postManager, IAppUser user,AppPostModel post)
        {
            this._postManager = postManager;
            this._user = user;
            this._post = post;
            InitializeComponent();
        }

        private void CommentButton_Click(object sender, EventArgs e)
        {
            this._postManager.AddCommentToPost(this._post.PostId, this._user.UserId, CommentText.Text);
            this.Close();
        }
    }
}
