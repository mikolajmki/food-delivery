using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace food_delivery.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode{ get; set; }
    }
}
