using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticulosWeb.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(String roleName) : base(roleName) { }
        public ApplicationRole(String roleName, string description, DateTime createDate)
            : base(roleName)
        {
            base.Name = roleName;
            Description = description;
            CreatedDate = createDate;
        }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
