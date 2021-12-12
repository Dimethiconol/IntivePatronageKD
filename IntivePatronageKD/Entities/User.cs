using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntivePatronageKD.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public decimal? Weight { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
