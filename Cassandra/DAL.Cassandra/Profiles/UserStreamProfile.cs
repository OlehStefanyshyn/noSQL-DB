using AutoMapper;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Profiles
{
    public class UserStreamProfile : Profile
    {
        public UserStreamProfile()
        {
            CreateMap<List<CUserDTO>, UserStreamDTO>()
                .ForMember(dest => dest.UserID, scr => scr.MapFrom(p => p.Count != 0 ? p[0].UserID : 0))
                .ForMember(dest => dest.Stream, scr => scr.MapFrom(p => ConvertToPosts(p)));



        }
        private List<CPostDTO> ConvertToPosts(List<CUserDTO> stream)
        {
            var ret = new List<CPostDTO>();
            foreach (var p in stream)
            {
                ret.Add(new CPostDTO()
                {
                    Title = p.Title,
                    Body = p.Body,
                    Comments = p.Comments,
                    PostCreateDate = p.PostCreateDate,
                    Like = p.Like,
                    Users = p.Users,
                    Tags = p.Tags,
                    Id = p.Id
                });
            }

            return ret;


        }
    }
}