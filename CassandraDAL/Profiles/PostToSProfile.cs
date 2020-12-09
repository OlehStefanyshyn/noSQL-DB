using AutoMapper;
using CassandraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDAL.Profiles
{
    public class PostToSProfile:Profile
    {
        public PostToSProfile()
        {
            CreateMap<SDTO, PostDTO>().ReverseMap();
        }
    }
}
