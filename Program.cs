// Asset tracking system

List<Asset> assets = new List<Asset>();

while (true)
{
    Console.WriteLine("\n=================================================");
    Console.WriteLine("COMPANY ASSET TRACKING SYSTEM");
    Console.WriteLine("=================================================");
    Console.WriteLine("\n1. Add Asset\n2. View Assets\n3. Search Assets\n4. Remove Asset\n5. Exit\n");
    Console.Write("Select option: ");
    String choice = Console.ReadLine();
    if (choice == "5" || choice.ToLower().Trim() == "q" || choice.ToLower().Trim() == "quit")
    {
        break;
    }
    if (choice == "1")
    {
        AddAssets();
    }
    else if (choice == "2")
    {
        ShowAssets(assets);
    }
    else if (choice == "3")
    {
        SearchAssets(); 
    }
    else if (choice == "4")
    {
        RemoveAsset(); 
    }
    else
    {
        Console.WriteLine("ERROR: Invalid Option");
    }
}

void RemoveAsset()
{
    Console.WriteLine("Type 'back' to go back to the menu.\nUse ID from the list to remove an asset.\n");
    ShowAssets(assets);  
    while (true)
    {
        
        Console.Write("Remove: "); 
        var enteredValue = Console.ReadLine(); 
        Console.WriteLine();

        if (enteredValue.ToLower().Trim() == "back")
        {
            break; 
        }

        if(!int.TryParse(enteredValue, out int number) || number < 0){
            Console.WriteLine("ERROR: Invalid ID"); 
            continue; 
        }

        Asset? asset = assets.FirstOrDefault(delegate (Asset asset)
        {
            return asset.ID == number; 
        }); 

        if (asset == null)
        {
            Console.WriteLine("No such asset found"); 
            continue; 
        } 
        
            assets.Remove(asset); 
            Console.WriteLine("Asset removed from the list"); 
            ShowAssets(assets); 
    }
}

void SearchAssets()
{
    Console.WriteLine("Type 'back' to go back to the menu."); 
    while (true)
    {
        
        Console.Write("Search: "); 
        var enteredValue = Console.ReadLine(); 
        Console.WriteLine();

        if (enteredValue.ToLower().Trim() == "back")
        {
            break; 
        }

        enteredValue = enteredValue.ToLower().Trim(); 

        List<Asset> matches =  assets.Where(delegate(Asset asset)
        {
            return asset.Brand.Contains(enteredValue, StringComparison.OrdinalIgnoreCase) || asset.Model.Contains(enteredValue, StringComparison.OrdinalIgnoreCase); 
        }).ToList(); 

        ShowAssets(matches); 

    }
}

void AddAssets()
{
    Console.WriteLine("\nEnter back at any prompt to return to the menu.");
    Console.WriteLine("Press Enter without a value to restart at Asset Type.");
    while (true)
    {
        Console.Write("Enter Asset Type (1 for Laptop, 2 for Mobile Phone): ");
        String assetType = Console.ReadLine();
        if (assetType == String.Empty)
        {
            continue;
        }
        if (assetType.ToLower().Trim() == "back")
        {
            break;
        }
        if (assetType != "1" && assetType != "2")
        {
            Console.WriteLine("ERROR: Invalid Asset Type");
            continue;
        }

        Console.Write("Enter Brand: ");
        String brand = Console.ReadLine();
        if (brand == String.Empty)
        {
            continue;
        }
        if (brand.ToLower().Trim() == "back")
        {
            break;
        }
    
        Console.Write("Enter Model: ");
        String model = Console.ReadLine();
        if (model.ToLower().Trim() == "back")
        {
            break;
        }
        if (model == String.Empty)
        {
            continue;
        }
    
        Console.Write("Enter Purchase Date (yyyy-MM-dd): ");
        String date = Console.ReadLine();
        if (date.ToLower().Trim() == "back")
        {
            break;
        }
        if (date == String.Empty)
        {
            continue;
        }
        if (!DateTime.TryParse(date, out DateTime purchaseDate))
        {
            Console.WriteLine("ERROR: Invalid Date");
            continue;
        }
    
        Console.Write("Enter Price (USD): ");
        String usd = Console.ReadLine();
        if (usd.ToLower().Trim() == "back")
        {
            break;
        }
        if (usd == String.Empty)
        {
            continue;
        }
        if (!double.TryParse(usd, out double priceUSD) || priceUSD < 0)
        {
            Console.WriteLine("ERROR: Invalid Price");
            continue;
        }
    
        Console.Write("Enter Office (Sweden, USA, Turkey): ");
        String enteredOffice = Console.ReadLine();
        if (enteredOffice.ToLower().Trim() == "back")
        {
            break;
        }
        if (enteredOffice == String.Empty)
        {
            continue;
        }
        if (!Enum.TryParse(enteredOffice, true, out Office office) || !Enum.IsDefined(office))
        {
            Console.WriteLine("ERROR: Invalid Office");
            continue;
        }

        Asset asset; 
        if (assetType == "1")
        {
            asset = new Laptop(brand, model, purchaseDate, priceUSD, office);            
        } else
        {
            asset = new MobilePhone(brand, model, purchaseDate, priceUSD, office); 
        }
        assets.Add(asset);
        Console.WriteLine("Asset added successfully!\n");
    }
}

void ShowAssets(List<Asset> list)
{
    Console.WriteLine("\nASSET LIST");
    if (list.Count == 0)
    {
        Console.WriteLine("No assets added yet.");
        return;
    }

    Console.WriteLine("---------------------------------------------------------------------------------------"); 
    Console.WriteLine("ID".PadRight(6) + "Office".PadRight(12) + "Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(12) + "Purchase Date".PadRight(16) + "Price".PadRight(12) + "Status");
    Console.WriteLine("---------------------------------------------------------------------------------------");

    List<Asset> sorted = list.OrderBy(delegate (Asset asset)
    {
        return asset.Office.ToString();  
    }).ThenBy(delegate(Asset asset)
    {
        return asset.PurchaseDate; 
    }).ToList(); 

    foreach (Asset asset in sorted)
    {
        DateTime eol = asset.EndOfLife(); 
        var today = DateTime.Today; 
        
        if (eol <= today.AddMonths(3))
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (eol <= today.AddMonths(6))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
 
        String status = Console.ForegroundColor == ConsoleColor.Red || Console.ForegroundColor == ConsoleColor.Yellow ? Console.ForegroundColor.ToString() : "";

        Console.WriteLine(asset.ID.ToString().PadRight(6) + asset.Office.ToString().PadRight(12) + asset.DisplayType().PadRight(12) + asset.Brand.PadRight(12) + asset.Model.PadRight(12) + asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(16) + (Math.Round(asset.PriceLocal).ToString() + " " + asset.Currency).PadRight(12) + status);
        Console.ResetColor();
    }
}

abstract class Asset {
    private static int id = 0; 
    public int ID { get; }
    public String Brand { get; set; }
    public String Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    private double _priceUSD; 
    public double PriceUSD { 
        get
        {
            return _priceUSD;     
        } 
        set
        {
            _priceUSD = value; 
            if (Office == Office.Sweden)
            {
                Currency = "SEK"; 
                PriceLocal = _priceUSD * 9.83; 
            }  
            else if (Office == Office.Turkey)
            {
                Currency = "TRY"; 
                PriceLocal = _priceUSD * 48.77; 
            }
            else
            {
                Currency = "USD"; 
                PriceLocal = _priceUSD; 
            }
        } }
    public double PriceLocal {get; set; }

    public String Currency { get; set; }
    public Office Office { get; set; }

    public DateTime EndOfLife()
    {
        return PurchaseDate.AddYears(3);
    }

    public TimeSpan AssetAge()
    {
        return DateTime.Today - PurchaseDate; 
    }

    public Asset (String brand, String model, DateTime purchaseDate, double priceUSD, Office office)
    {
        Brand = brand;
        Model = model;
        PurchaseDate = purchaseDate;
        Office = office;
        PriceUSD = priceUSD;
        ID = id; 
        id++; 
    }

    public abstract String DisplayType(); 

}

enum Office
{
    Sweden,
    USA, 
    Turkey
}

class Laptop : Asset
{
    public Laptop(String brand, String model, DateTime purchaseDate, double priceUSD, Office office)
        : base(brand, model, purchaseDate, priceUSD, office){}

    public override String DisplayType()
    {
        return "Laptop"; 
    }


}

class MobilePhone : Asset 
{
    public MobilePhone(String brand, String model, DateTime purchaseDate, double priceUSD, Office office)
        : base(brand, model, purchaseDate, priceUSD, office){}

    public override string DisplayType()
    {
        return "Phone"; 
    }

}


