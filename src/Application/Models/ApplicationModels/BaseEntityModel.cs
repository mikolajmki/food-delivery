using Application.Abstractions;

namespace Application.Models.ApplicationModels;

public class BaseEntityModel : IBaseEntityModel
{
    public int Id { get; set; }
}
