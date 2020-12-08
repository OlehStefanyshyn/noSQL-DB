using MongoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface IPostManager
    {
        List<PostDTO> GetAllPosts();
        PostDTO GetPostById(int post_id);
        void CreatePost(int Author_Id, string Title, string Body);
        void AddCommentToPost(int PostId, int AuthorId, string CommentText);
        void AddLikeToPost(int PostId, int UserId);
        void AddDislikeToPost(int PostId, int UserId);
    }
}