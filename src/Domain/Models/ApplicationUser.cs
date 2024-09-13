using Domain.Models.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser: IdentityUser, IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode{ get; set; }
    }
}
