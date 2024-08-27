using Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class ApplicationUserModel: IdentityUser
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode{ get; set; }
    }
}
