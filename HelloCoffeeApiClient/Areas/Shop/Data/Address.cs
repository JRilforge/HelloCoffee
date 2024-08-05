using System.ComponentModel.DataAnnotations;

namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class Address
{
    [Key]
    public Guid Id { get; set; }

    public string FirstLine { get; set; } = "";
    public string? SecondLine { get; set; }
    public string City { get; set; } = "London";
    public string? County { get; set; }
    public string Country { get; set; } = "United Kingdom";
    public string PostCode { get; set; } = "";
}