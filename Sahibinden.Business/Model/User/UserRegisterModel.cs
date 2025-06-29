﻿using AutoMapper;
using Sahibinden.Entities.Enums;

namespace Sahibinden.Business.Model.User
{
    [AutoMap(typeof(Entities.Concrete.User), ReverseMap = true)]
    public class UserRegisterModel
    {
        public bool Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime RecordDate { get; set; }
        public UserType UserType { get; set; }
    }
}
