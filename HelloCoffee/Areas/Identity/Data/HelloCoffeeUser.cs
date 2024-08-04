using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HelloCoffee.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HelloCoffeeUser class
public class HelloCoffeeUser : IdentityUser
{
    [JsonProperty(PropertyName = "UserId")]
    public Guid UserId { get; set; } = Guid.NewGuid();

    public int Type { get; set; }
}

