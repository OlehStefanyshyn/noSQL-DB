using AutoMapper;
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
    public partial class PostViewForm : Form
    {
        private readonly IUserManager _userManager;
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;
        private readonly IAppUser _user;
        private AppPostModel _post;
        public PostViewForm(IUserManager userManager, IPostManager postManager, IMapper mapper, IAppUser user,AppPostModel post)
        {

            this._userManager = userManager;
            this._mapper = mapper;
            this._postManager = postManager;
            this._user = user;
            this._post = post;
            InitializeComponent();
            UpdateData();
        }
        private void UpdateData()
        {
            this._post = _mapper.Map < AppPostModel > (this._postManager.GetPostById(this._post.PostId));
            Title.Text = _post.Title;
            Body.Text = _post.Body;
            LikeCount.Text = Convert.ToString(_post.Likes);
            DislikeCount.Text = Convert.ToString(_post.Dislikes);
        }

        private void LikeButton_Click(object sender, EventArgs e)
        {
            this._postManager.AddLikeToPost(this._post.PostId, this._user.UserId);
            UpdateData();
        }

        private void DislikeButton_Click(object sender, EventArgs e)
        {
            this._postManager.AddDislikeToPost(this._post.PostId, this._user.UserId);
            UpdateData();
        }

        private void CommentsButton_Click(object sender, EventArgs e)
        {
            var CommentListForm = new CommentList(_postManager, _mapper, _user, _post);
            CommentListForm.Show();
        }
    }
}
