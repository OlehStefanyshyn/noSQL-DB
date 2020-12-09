﻿using AutoMapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CassandraNetwork.Models.Profiles
{
    public class RegisterProfile:Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterModel, UsersDTO>().ReverseMap();
        }
    }
}