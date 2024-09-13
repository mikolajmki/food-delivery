<<<<<<<< HEAD:Application/Application/Models/ApplicationModels/ItemModel.cs
﻿namespace Application.Models.ApplicationModels;

public class ItemModel : BaseEntityModel
========
﻿namespace Presentation.ApiModels;

public class ItemApiModel : BaseApiModel
>>>>>>>> develop:src/WebAPI/ApiModels/ItemApiModel.cs
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public CategoryApiModel Category { get; set; }
    public SubcategoryApiModel Subcategory { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }

}
