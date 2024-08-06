using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Newtonsoft.Json;

namespace HelloCoffeeTestSuite.Areas;

public static class HelloCoffeeTestExtensionMethods
{
    public static string ToJson(this AddItemToBasketRequest source)
    {
        return JsonConvert.SerializeObject(source);
    }
    
    public static string ToJson(this Address source)
    {
        return JsonConvert.SerializeObject(source);
    }
    
    public static TResponseType? ToType<TResponseType>(this string source)
    {
        return JsonConvert.DeserializeObject<TResponseType>(source);
    }
}