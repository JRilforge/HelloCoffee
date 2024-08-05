namespace HelloCoffeeApiClient.Areas.Shop.Data.Dto;

public class CreateOrderRequest
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public Address? Address { get; set; }

    public string CardNumber { get; set; } = "";
    public string NameOnCard { get; set; } = "";
    public string Expiration { get; set; } = "";
    public string Cvv { get; set; } = "";
}