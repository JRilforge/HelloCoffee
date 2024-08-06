using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloCoffee.Pages;

public class CheckoutModel : PageModel
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = "John";
    public string LastName { get; set; } = "Doe";

    public Address Address { get; set; } = new()
    {
        FirstLine = "1 Imaginary Street",
        SecondLine = "Fake Avenue",
        City = "London",
        County = "Greater London",
        Country = "United Kingdom",
        PostCode = "ST6 2AH"
    };

    public string CardNumber { get; set; } = "7894-3244-3242-5489";
    public string NameOnCard { get; set; } = "Mr John Doe";
    public string Expiration { get; set; } = "12/2040";
    public string Cvv { get; set; } = "368";
    
    public void OnGet()
    {
        
    }
}