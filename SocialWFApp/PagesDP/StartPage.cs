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
using Unity;
using Unity.Injection;
using Unity.Resolution;

namespace SocialWFmApp
{
    public partial class StartPage : Form
    {
        private readonly IUserManager _userManager;
        private readonly IPostManager _postManager;
        private readonly IMapper _mapper;
        private readonly IAppUser _user;
        public StartPage(IUserManager userManager, IPostManager postManager,IMapper mapper,IAppUser user)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._postManager = postManager;
            this._user = user;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var us = new List<AppUserModel>();
            foreach(var u in this._userManager.GetAllUsers())
            {
                us.Add(_mapper.Map<AppUserModel>(u));
            }
            var UListForm = new UserList(this._userManager, us,this._user);
            UListForm.Show();
        }

        private void PostListButton_Click(object sender, EventArgs e)
        {
            var postListForm = new PostListForm(this._postManager, _mapper);
            postListForm.Show();
        }

        private void MyPageButton_Click(object sender, EventArgs e)
        {
            var MyPageForm = new MyPage(_mapper.Map<AppUserModel>( this._userManager.GetUserById(this._user.UserId)), this._user);
            MyPageForm.Show();
        }
    }
}
