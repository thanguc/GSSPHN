using System;
using System.Collections.Generic;

namespace EmptyWeb.Models
{
    public class Userrole
    {
        public Guid UserroleID { get; set; }
        public string Roletitle { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
