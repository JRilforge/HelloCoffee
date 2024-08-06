using Newtonsoft.Json;

namespace HelloCoffeeApiClient.Areas.Shop.Data.Dto;

public class AddItemToBasketRequest
{
    [JsonProperty("userId")]
    public Guid UserId { get; set; }
    [JsonProperty("itemId")]
    public Guid ItemId { get; set; }
    [JsonProperty("unitCountModificationId")]
    public int UnitCountModification { get; set; }
}