using AutoMapper;
using DTO.Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cassandra.Profiles
{
    class PostStreamProfile
    {
        public PostStreamProfile()
        {
            CreateMap<StreamDTO, CPostDTO>().ReverseMap();
        }
    }
}
