using AutoMapper;
using CassandraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Profiles
{
    public class UserSProfile:Profile
    {
        public UserSProfile()
        {
            CreateMap<List<UserS>, UserSDTO>()
                .ForMember(dest => dest.User_Id, scr => scr.MapFrom(p => p.Count != 0 ? p[0].User_Id : 0))
                .ForMember(dest => dest.S, scr => scr.MapFrom(p => ConvertToPosts(p)));



        }
        private List<PostDTO> ConvertToPosts(List<UserS> s)
        {
            var ret = new List<PostDTO>();
            foreach(var p in stream)
            {
                ret.Add( new PostDTO()
                {
                    Title = p.Title,
                    Body = p.Body,
                    Comment = p.Comment,
                    Create_Date = p.Create_Date,
                    Dislikes = p.Dislikes,
                    Likes = p.Likes,
                    Modify_Date = p.Modify_Date,
                    Author_Id = p.Author_Id,
                    Post_Id = p.Post_Id
                });
            }

            return ret;
            

        }
    }
}
