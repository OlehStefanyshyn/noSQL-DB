using AutoMapper;
using BusinessLogic.Interface;
using SocialWinFormApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace SocialWFApp
{
    public partial class PostListForm : Form
    {
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;
        private List<AppPostModel> _postsList;
        public PostListForm(IPostManager postManager,IMapper mapper)
        {
            this._postManager = postManager;
            this._mapper = mapper;
            InitializeComponent();
            RefreshGrid();
            //this._postManager.CreatePost(1, "Post Title", "Post Body");
        }
        private void RefreshGrid()
        {
            var all_posts = this._postManager.GetAllPosts();
            var out_posts = new List<AppPostModel>();
            foreach(var post in all_posts)
            {
                out_posts.Add(_mapper.Map<AppPostModel>(post));
            }
            _postsList = out_posts;
            var PostsBL = new BindingList<AppPostModel>(out_posts);

            PostsBS.DataSource = PostsBL;

            Postsnav.BindingSource = PostsBS;
            PostsDGV.DataSource = PostsBS;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var postFrom = SocialWinFormApp.Program.Container.Resolve<AddPost>();
            postFrom.ShowDialog();
            RefreshGrid();
        }

        private void PostsDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var PostViewForm = Program.Container.Resolve<PostViewForm>(new ParameterOverride("post",this._postsList[e.RowIndex]));
            PostViewForm.Show();
        }
    }
}
