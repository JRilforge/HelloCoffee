namespace HelloCoffeeApiClient.Areas.Shop.Data.Type;

public class ShopItemConstants
{
    public static readonly ShopItem[] Coffee = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b942"),
            Name = "Cappuccino",
            Price = 7.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b943"),
            Name = "Americano",
            Price = 9.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b944"),
            Name = "Latte",
            Price = 6.70
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b945"),
            Name = "Mocha",
            Price = 10.50
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b946"),
            Name = "Espresso",
            Price = 7.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b947"),
            Name = "Macchiato",
            Price = 11.60
        }
    };
    
    public static readonly ShopItem[] Tea = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b948"),
            Name = "Green",
            Price = 5.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b949"),
            Name = "English",
            Price = 3.50
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b940"),
            Name = "Lemon",
            Price = 8.50
        }
    };
    
    public static readonly ShopItem[] HotChocolate = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b950"),
            Name = "Hot Chocolate",
            Price = 10.40
        }
    };

    public static readonly ShopItem[] Juice = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b951"),
            Name = "Apple Juice",
            Price = 7.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b952"),
            Name = "Orange Juice",
            Price = 5.90
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b953"),
            Name = "Grape Juice",
            Price = 4.55
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b954"),
            Name = "Pineapple Juice",
            Price = 8.34
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b955"),
            Name = "Mango Juice",
            Price = 5.25
        }
    };

    public static readonly ShopItem[] BottledWater = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b956"),
            Name = "Still Water",
            Price = 4.00
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b957"),
            Name = "Sparkling Water",
            Price = 3.50
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b958"),
            Name = "Mineral Water",
            Price = 5.57
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b959"),
            Name = "Spring Water",
            Price = 7.65
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b960"),
            Name = "Distilled Water",
            Price = 2.87
        }
    };

    public static readonly ShopItem[] Sandwich = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b961"),
            Name = "Classic BLT",
            Price = 4.50
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b962"),
            Name = "Turkey Club",
            Price = 6.49
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b963"),
            Name = "Veggie Delight",
            Price = 3.46
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b964"),
            Name = "Pastrami Reuben",
            Price = 7.90
        },
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b965"),
            Name = "Caprese Panini",
            Price = 9.30
        }
    };

    private static readonly ShopItem[] Pastry = new[]
    {
        // Pastries
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b966"),
            Name = "Choux Pastry",
            Price = 8.40
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b967"),
            Name = "Chocolate Choux Pastry",
            Price = 10.54
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b968"),
            Name = "Cream Puffs",
            Price = 5.43
        },

// Desserts
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b969"),
            Name = "Sweet Crepes (Crêpes Sucrées)",
            Price = 7.45
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b970"),
            Name = "Galettes",
            Price = 3.46
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b971"),
            Name = "Pâte Sucrée (Sweet Shortcrust Pastry)",
            Price = 7.45
        },
    };

    public static readonly ShopItem[] Snack = new[]
    {
        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b972"),
            Name = "Chocolate Chip Cookies",
            Price = 4.50
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b973"),
            Name = "Popcorn",
            Price = 2.30
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b974"),
            Name = "Trail Mix",
            Price = 3.90
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b975"),
            Name = "Crisps",
            Price = 0.50
        },

        new ShopItem()
        {
            Id = Guid.Parse("71a5bb26-d729-41bd-9519-5197f7d6b976"),
            Name = "Granola Bars",
            Price = 1.50
        }
    };

    public static readonly Dictionary<ItemSubCategory, ShopItem[]> SubCategoryItemsMap = new()
    {
        { ItemSubCategory.Coffee, Coffee },
        { ItemSubCategory.Tea, Tea },
        { ItemSubCategory.HotChocolate, HotChocolate },
        { ItemSubCategory.Juice, Juice },
        { ItemSubCategory.BottledWater, BottledWater },
        { ItemSubCategory.Sandwich, Sandwich },
        { ItemSubCategory.Pastry, Pastry },
        { ItemSubCategory.Snack, Snack }
    };

    static ShopItemConstants()
    {
        foreach (var subCategoryItemsEntry in ShopItemConstants.SubCategoryItemsMap)
        {
            var subCategory = subCategoryItemsEntry.Key;
            var category = GetCategory(subCategory);
                
            foreach (var item in subCategoryItemsEntry.Value)
            {
                item.Category = (int) category;
                item.SubCategory = (int) subCategory;
            }    
        }
    }

    public static readonly Dictionary<Guid, ShopItem> ItemMap = SubCategoryItemsMap.Values.SelectMany(i => i)
        .ToDictionary(x => x.Id, x => x);

    public static ItemCategory GetCategory(ItemSubCategory source)
    {
        return ((int) source < 5 ? ItemCategory.Drink : ItemCategory.Food);
    }

    public static readonly int ItemCount = Coffee.Length + Tea.Length + HotChocolate.Length + BottledWater.Length +
               Juice.Length + Sandwich.Length + Pastry.Length + Snack.Length;
}

