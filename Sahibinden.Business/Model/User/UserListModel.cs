﻿using Sahibinden.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Model.User
{
    public class UserListModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public UserType UserType { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RecordDate { get; set; }

    }
}
