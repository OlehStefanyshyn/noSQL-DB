using AutoMapper;
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
    public partial class CommentList : Form
    {
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;
        private readonly IAppUser _user;
        private AppPostModel _post;
        public CommentList(IPostManager postManager, IMapper mapper,IAppUser user,AppPostModel post)
        {
            this._mapper = mapper;
            this._postManager = postManager;
            this._user = user;
            this._post = post;
            this._postManager.AddCommentToPost(post.PostId, _user.UserId, "Some comment");
            InitializeComponent();
            RefreshGrid();
           
        }
        private void RefreshGrid()
        {
            this._post = this._mapper.Map<AppPostModel>(_postManager.GetPostById(this._post.PostId));
            var CommentsBL = new BindingList<CommentDTO>(_post.Comments);
            CommentListBS.DataSource = CommentsBL;

            CommentListBN.BindingSource = CommentListBS;
            CommentListDGV.DataSource = CommentListBS;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var AddCommnetForm = new AddComment(_postManager, _user, _post);
            AddCommnetForm.Show();
            RefreshGrid();
        }

        private void CommentListDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var ViewComment = new ViewComment(this._post.Comments[e.RowIndex]);
            ViewComment.Show();
        }
    }
}
