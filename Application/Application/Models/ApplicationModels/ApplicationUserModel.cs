using Application.Abstractions;

namespace Application.Models.ApplicationModels
{
    public class ApplicationUserModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}
