using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticulosWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public int IdPer { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String Province { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
    }
}
