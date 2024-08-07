using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace food_delivery.Models
{
    public class Category: BaseEntity
    {
        public string Title { get; set; }    

    }
}
