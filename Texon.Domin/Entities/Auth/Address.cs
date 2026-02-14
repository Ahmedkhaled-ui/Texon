using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texon.Domin.Entities.Auth
{
    public class Address
    {

        public ApplicationUser user { get; set; }
        public string userId { get; set; } // fk
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
