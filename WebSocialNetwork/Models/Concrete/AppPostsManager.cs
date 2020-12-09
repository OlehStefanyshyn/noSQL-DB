﻿using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CassandraNetwork.Models.Interfaces;
using CassandraNetwork.Models.Profiles;

namespace CassandraNetwork.Models.Concrete
{
    public class AppPostsManager: IAppPostsManager
    {
        private readonly IMapper _mapper;
        private readonly IPostManager _postManager;
        private readonly IUserManager _userManager;
        private readonly IStreamManager _streamManager;
        /*
        public AppPostsManager()
        {
            this._mapper = ConfigureMapper();
            this._postManager = new PostManager();
        }
        */
        public AppPostsManager(IPostManager postManager, IUserManager userManager,IStreamManager streamManager)
        {
            this._userManager = userManager;
            this._postManager = postManager;
            this._streamManager = streamManager;
            this._mapper = ConfigureMapper();
            
        }

        private IMapper ConfigureMapper()
        {
            var conf = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PostProfile(_userManager));
            });
            return conf.CreateMapper();

        }
        /*
        public void AddCommentToPost(int PostId,int UserId, string CommentText)
        {
            this._postManager.AddCommentToPost(PostId, UserId, CommentText);
        }

        public void CreatePost(int UserId, PostModel post)
        {
            this._postManager.CreatePost(UserId, post.Title, post.Body, post.Tags);
        }

        public void DislikePost(int PostId, int UserId)
        {
            this._postManager.DislikePost(PostId, UserId);
        }

      

        public void LikePost(int PostId, int UserId)
        {
            this._postManager.LikePost(PostId, UserId);
        }
        */
        public List<PostModel> GetAllPosts()
        {
            var all_posts = new List<PostModel>();

            foreach (var p in this._postManager.GetAllPosts())
            {
                all_posts.Add(this._mapper.Map<PostModel>(p));
            }
            return all_posts;
        }

        public void LikePost(Guid PostId, int UserId)
        {
            this._postManager.LikePost(PostId, UserId);
        }

        public void DislikePost(Guid PostId, int UserId)
        {
            this._postManager.DislikePost(PostId, UserId);
        }

        public void AddCommentToPost(Guid PostId, int UserId, string CommentText)
        {
            this._postManager.AddCommentToPost(PostId, UserId, CommentText);
        }

        public void CreatePost(int UserId, PostModel post)
        {
            this._postManager.CreatePost(UserId, post.Title, post.Body);
        }

        public List<PostModel> GetUserStream(long id)
        {
            var user_steram = new List<PostModel>();
            foreach(var p in this._streamManager.GetStreamForUser(id))
            {
                user_steram.Add(_mapper.Map<PostModel>(p));
            }
            return user_steram;
        }
    }
}
