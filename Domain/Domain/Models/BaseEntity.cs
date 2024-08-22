using Domain.Models.Abstraction;

namespace Domain.Models;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}
