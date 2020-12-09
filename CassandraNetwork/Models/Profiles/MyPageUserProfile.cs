using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CassandraNetwork.Models.Profiles
{
    public class MyPageUserProfile:Profile
    {
        public MyPageUserProfile()
        {
            CreateMap<MyPageUserModel, UsersDTO>().ReverseMap();
        }
    }
}