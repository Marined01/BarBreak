﻿using System.Collections.Generic;

namespace BarBreak.Core.Entities
{
    public class Role
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}