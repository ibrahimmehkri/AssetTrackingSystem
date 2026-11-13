// Asset tracking system
using System.ComponentModel.Design.Serialization;

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
        using (var context = new AppDbContext())
        {
            List<Asset> assets = context.Assets.ToList(); 
            ShowAssets(assets);    
        }
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
    using (var context = new AppDbContext())
    {
        List<Asset> assets = context.Assets.ToList(); 
        ShowAssets(assets);   
    }  
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

        using (var context = new AppDbContext())
        {
            Asset? asset = context.Assets.FirstOrDefault(delegate (Asset asset)
            {
                return asset.ID == number; 
            }); 

            if (asset == null)
            {
                Console.WriteLine("No such asset found"); 
                continue; 
            } 
            
            context.Assets.Remove(asset);
            context.SaveChanges();  
            Console.WriteLine("Asset removed from the list"); 
            ShowAssets(context.Assets.ToList()); 
        }
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

        using (var context = new AppDbContext())
        {
            List<Asset> matches =  context.Assets.Where(delegate(Asset asset)
            {
                return asset.Brand.Contains(enteredValue, StringComparison.OrdinalIgnoreCase) || asset.Model.Contains(enteredValue, StringComparison.OrdinalIgnoreCase); 
            }).ToList(); 

            ShowAssets(matches); 
        }
    }
}

void AddAssets()
{
    Console.WriteLine("\nEnter back at any prompt to return to the menu.");
    Console.WriteLine("Press Enter without a value to restart at Asset Type.");
    while (true)
    {
        Console.Write("Enter Asset Type (1: Computer, 2: Mobile): ");
        String assetType = Console.ReadLine();
        String subAssetType = ""; 
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

        if (assetType == "1")
        {
            while (true)
            {
                Console.Write("Enter Computer Type (1: Laptop, 2: Desktop): ");
                subAssetType = Console.ReadLine();
                if (subAssetType == String.Empty)
                {
                    continue;
                }
                if (subAssetType.ToLower().Trim() == "back")
                {
                    break;
                }
                if (subAssetType != "1" && subAssetType != "2")
                {
                    Console.WriteLine("ERROR: Invalid Computer Type");
                    continue;
                }
                break; 
            }
            
        } else if (assetType == "2")
        {
            while (true)
            {
                Console.Write("Enter Mobile Type (1: Phone, 2: Tablet): ");
                subAssetType = Console.ReadLine();
                if (subAssetType == String.Empty)
                {
                    continue;
                }
                if (subAssetType.ToLower().Trim() == "back")
                {
                    break;
                }
                if (subAssetType != "1" && subAssetType != "2")
                {
                    Console.WriteLine("ERROR: Invalid Mobile Type");
                    continue;
                }
                break;
            }
            
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
    
        Console.Write("Enter Office (1: Sweden, 2: USA, 3: Turkey): ");
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
            if (subAssetType == "1")
            {
                asset = new Computer(brand, model, purchaseDate, priceUSD, office, ComputerAsset.Laptop);
            } else 
            {
                asset = new Computer(brand, model, purchaseDate, priceUSD, office, ComputerAsset.Desktop);
            }
                       
        } else
        {
             if (subAssetType == "1")
            {
                asset = new Mobile(brand, model, purchaseDate, priceUSD, office, MobileAsset.Phone);
            } else 
            {
                asset = new Mobile(brand, model, purchaseDate, priceUSD, office, MobileAsset.Tablet);
            }
        }
        
        using (var context = new AppDbContext())
        {
            context.Assets.Add(asset); 
            context.SaveChanges(); 
        }
        
        Console.WriteLine($"Asset ID {asset.ID} was added successfully!\n");
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

    Console.WriteLine("------------------------------------------------------------------------------------------"); 
    Console.WriteLine("ID".PadRight(6) + "Office".PadRight(12) + "Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(12) + "Purchase Date".PadRight(16) + "Price".PadRight(12) + "Status");
    Console.WriteLine("------------------------------------------------------------------------------------------");

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




