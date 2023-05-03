﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public decimal AccountBalance { get; set; }

    }
}
