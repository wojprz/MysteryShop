﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryShop.Infrastructure.Models
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
