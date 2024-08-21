using Application.Abstractions;

namespace Application.Models;

public class BaseEntityModel : IBaseEntityModel
{
    public int Id { get; set; }
}
