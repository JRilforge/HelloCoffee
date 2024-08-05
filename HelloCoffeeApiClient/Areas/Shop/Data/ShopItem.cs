using System.ComponentModel.DataAnnotations;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Newtonsoft.Json;

namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class ShopItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] 
    public string Name { get; set; } = "";
    
    [Required]
    public double? Price { get; set; } = 0.0;

    [Required]
    public int Category { get; set; }
    
    [Required]
    public int SubCategory { get; set; }
}