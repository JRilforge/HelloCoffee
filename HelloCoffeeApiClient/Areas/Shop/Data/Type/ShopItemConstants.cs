namespace HelloCoffeeApiClient.Areas.Shop.Data.Type;

public class ShopItemConstants
{
    public static ShopItem[] Coffee = new[]
    {
        new ShopItem()
        {
            Name = "Cappuccino"
        },
        new ShopItem()
        {
            Name = "Americano"
        },
        new ShopItem()
        {
            Name = "Latte"
        },
        new ShopItem()
        {
            Name = "Mocha"
        },
        new ShopItem()
        {
            Name = "Espresso"
        },
        new ShopItem()
        {
            Name = "Macchiato"
        }
    };
    
    public static ShopItem[] Tea = new[]
    {
        new ShopItem()
        {
            Name = "Green"
        },
        new ShopItem()
        {
            Name = "English"
        },
        new ShopItem()
        {
            Name = "Lemon"
        }
    };
    
    public static ShopItem[] HotChocolate = new[]
    {
        new ShopItem()
        {
            Name = "Hot Chocolate"
        }
    };

    public static ShopItem[] Juice = new[]
    {
        new ShopItem()
        {
            Name = "Apple Juice"
        },
        new ShopItem()
        {
            Name = "Orange Juice"
        },
        new ShopItem()
        {
            Name = "Grape Juice"
        },
        new ShopItem()
        {
            Name = "Pineapple Juice"
        },
        new ShopItem()
        {
            Name = "Mango Juice"
        }
    };

    public static ShopItem[] BottledWater = new[]
    {
        new ShopItem()
        {
            Name = "Still Water"
        },
        new ShopItem()
        {
            Name = "Sparkling Water"
        },
        new ShopItem()
        {
            Name = "Mineral Water"
        },
        new ShopItem()
        {
            Name = "Spring Water"
        },
        new ShopItem()
        {
            Name = "Distilled Water"
        }
    };

    public static ShopItem[] Sandwiches = new[]
    {
        new ShopItem()
        {
            Name = "Classic BLT"
        },
        new ShopItem()
        {
            Name = "Turkey Club"
        },
        new ShopItem()
        {
            Name = "Veggie Delight"
        },
        new ShopItem()
        {
            Name = "Pastrami Reuben"
        },
        new ShopItem()
        {
            Name = "Caprese Panini"
        }
    };

    private static ShopItem[] Pastries = new[]
    {
        // Pastries
        new ShopItem()
        {
            Name = "Choux Pastry"
        },

        new ShopItem()
        {
            Name = "Chocolate Choux Pastry"
        },

        new ShopItem()
        {
            Name = "Cream Puffs"
        },

// Desserts
        new ShopItem()
        {
            Name = "Sweet Crepes (Crêpes Sucrées)"
        },

        new ShopItem()
        {
            Name = "Galettes"
        },

        new ShopItem()
        {
            Name = "Pâte Sucrée (Sweet Shortcrust Pastry)"
        },
    };

    public static ShopItem[] Snacks = new[]
    {
        new ShopItem()
        {
            Name = "Chocolate Chip Cookies"
        },

        new ShopItem()
        {
            Name = "Popcorn"
        },

        new ShopItem()
        {
            Name = "Trail Mix"
        },

        new ShopItem()
        {
            Name = "Crisps"
        },

        new ShopItem()
        {
            Name = "Granola Bars"
        }
    };
}

