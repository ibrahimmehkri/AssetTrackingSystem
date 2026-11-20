// Asset tracking system
using System.ComponentModel.Design.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;

while (true)
{
    Console.WriteLine("\n=================================================");
    Console.WriteLine("COMPANY ASSET TRACKING SYSTEM");
    Console.WriteLine("=================================================");
    Console.WriteLine("\n1. Add Asset\n2. View Assets\n3. Search Assets\n4. Remove Asset\n5. Update Asset\n6. Show Report\n7. Exit\n");
    Console.Write("Select option: ");
    String choice = Console.ReadLine();
    if (choice == "7" || choice.ToLower().Trim() == "q" || choice.ToLower().Trim() == "quit")
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
    else if(choice == "5")
    {
        UpdateAsset(); 
    }
    else if(choice == "6")
    {
        while (true)
        {
            Console.WriteLine("1. Office Report\n2. Overview Report");
            var reportChoice = Console.ReadLine();  
            switch (reportChoice)
            {
                case "1": 
                    OfficeReport();
                    break;
                case "2": 
                    OverviewReport(); 
                    break; 
                default: 
                    Console.WriteLine("Invalid Choice."); 
                    continue; 
            }

            break;
        }
    }
    else if(choice == "7")
    {
        break;
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

void AddAssets(Asset? existingAsset = null)
{
    Console.WriteLine("\nEnter back at any prompt to return to the menu.");
    if (existingAsset != null)
    {
        Console.WriteLine("Press Enter to keep the value that current asset has.");
    }
    while (true)
    {
        String enterAssetType = existingAsset != null ? 
            $"Current Asset type {existingAsset.GetType().Name}, Enter New Asset Type (1: Computer, 2: Mobile): " : 
            "Enter Asset Type (1: Computer, 2: Mobile): "; 

        Console.Write(enterAssetType);
        String assetType = Console.ReadLine();
        String subAssetType = ""; 
        if (assetType == String.Empty)
        {
            ;
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

    List<Asset> computers = list
        .Where(a => a is Computer)
        .OrderBy(a => a.PurchaseDate)
        .ToList();

    List<Asset> mobiles = list
        .Where(a => a is Mobile)
        .OrderBy(a => a.PurchaseDate)
        .ToList();

    void PrintGroup(string title, List<Asset> group)
    {

        Console.WriteLine(title);
        Console.WriteLine(new string('-', 72));
        Console.WriteLine($"{"ID",-5}{"Brand",-12}{"Model",-20}{"Purchase Date",-18}{"Status",-10}");
        Console.WriteLine(new string('-', 72));

        foreach (Asset asset in group)
        {

            // This is where u check the status

            switch (asset.GetStatus())
            {
                case Status.EXPIRED:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case Status.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case Status.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case Status.NORMAL:
                    Console.ResetColor();
                    break;
            }
           
            Console.WriteLine(
                $"{asset.ID,-5}" +
                $"{asset.Brand,-12}" +
                $"{asset.Model,-20}" +
                $"{asset.PurchaseDate,-18:yyyy-MM-dd}" + 
                $"{asset.GetStatus(),-10}");

                Console.ResetColor();
        }
        Console.WriteLine(); 
    }

    PrintGroup("Computers", computers);
    PrintGroup("Mobile Devices", mobiles);
    Console.WriteLine();

}

void UpdateAsset()
{
    using (var context = new AppDbContext())
    {
        List<Asset> list = context.Assets.ToList(); 
        ShowAssets(list); 
    }

    while (true)
    {
        Console.WriteLine("Please enter an ID from the list above: ");
        var IDString = Console.ReadLine();

        if(int.TryParse(IDString, out int id))
        {
            using (var context = new AppDbContext())
            {
                Asset? asset = context.Assets.FirstOrDefault(a => a.ID == id); 
                if (asset != null)
                {
                    Console.WriteLine(new string('-', 72));
                    Console.WriteLine($"{"ID",-5}{"Brand",-12}{"Model",-20}{"Purchase Date",-18}{"Status",-10}");
                    Console.WriteLine(new string('-', 72));
                    Console.WriteLine(
                        $"{asset.ID,-5}" +
                        $"{asset.Brand,-12}" +
                        $"{asset.Model,-20}" +
                        $"{asset.PurchaseDate,-18:yyyy-MM-dd}" + 
                        $"{asset.GetStatus(),-10}"); 

                    Console.WriteLine(); 

                    // Push everything down here. 
                    Console.WriteLine("Leave blank and press enter to keep current values"); 
                    Console.Write($"Current Brand ({asset.Brand}), Enter Brand: ");
                    String brand = Console.ReadLine();
                    if (brand == String.Empty)
                    {
                        brand = asset.Brand;
                    }
                    if (brand.ToLower().Trim() == "back")
                    {
                        break;
                    }
                
                    Console.Write($"Current Model ({asset.Model}), Enter Model: ");
                    String model = Console.ReadLine();
                    if (model.ToLower().Trim() == "back")
                    {
                        break;
                    }
                    if (model == String.Empty)
                    {
                        model = asset.Model;
                    }

                    DateTime purchaseDate = asset.PurchaseDate;
                    while (true)
                    {
                        Console.Write($"Current Purchase date ({asset.PurchaseDate:yyyy-MM-dd}), Enter Purchase Date (yyyy-MM-dd): ");
                        String date = Console.ReadLine();
                        if (date.ToLower().Trim() == "back")
                        {
                            return;
                        }
                        if (date == String.Empty)
                        {
                            purchaseDate = asset.PurchaseDate;
                            break;
                        }
                        if (!DateTime.TryParse(date, out purchaseDate))
                        {
                            Console.WriteLine("ERROR: Invalid Date");
                            continue;
                        }
                        break;
                    }

                    double priceUSD = asset.PriceUSD;
                    while (true)
                    {
                        Console.Write($"Current Price ({asset.PriceUSD}), Enter Price (USD): ");
                        String usd = Console.ReadLine();
                        if (usd.ToLower().Trim() == "back")
                        {
                            return;
                        }
                        if (usd == String.Empty)
                        {
                            priceUSD = asset.PriceUSD;
                            break;
                        }
                        if (!double.TryParse(usd, out priceUSD) || priceUSD < 0)
                        {
                            Console.WriteLine("ERROR: Invalid Price");
                            continue;
                        }
                        break;
                    }

                    Office office = asset.Office;
                    while (true)
                    {
                        Console.Write($"Current Office ({asset.Office}), Enter Office (1: Sweden, 2: USA, 3: Turkey): ");
                        String enteredOffice = Console.ReadLine();
                        if (enteredOffice.ToLower().Trim() == "back")
                        {
                            return;
                        }
                        if (enteredOffice == String.Empty)
                        {
                            office = asset.Office;
                            break;
                        }
                        if (!Enum.TryParse(enteredOffice, true, out office) || !Enum.IsDefined(office))
                        {
                            Console.WriteLine("ERROR: Invalid Office");
                            continue;
                        }
                        break;
                    }

                    asset.Brand = brand; 
                    asset.Model = model; 
                    asset.PurchaseDate = purchaseDate; 
                    asset.Office = office; 
                    asset.PriceUSD = priceUSD; 

                    context.SaveChanges(); 

                    Asset? savedAsset = context.Assets.FirstOrDefault(a => a.ID == id); 
                    if (asset != null)
                    {
                        Console.WriteLine(); 
                        Console.WriteLine("Saved as follows: ");
                        Console.WriteLine(new string('-', 72));
                        Console.WriteLine($"{"ID",-5}{"Brand",-12}{"Model",-20}{"Purchase Date",-18}{"Status",-10}");
                        Console.WriteLine(new string('-', 72));
                        Console.WriteLine(
                            $"{savedAsset.ID,-5}" +
                            $"{savedAsset.Brand,-12}" +
                            $"{savedAsset.Model,-20}" +
                            $"{savedAsset.PurchaseDate,-18:yyyy-MM-dd}" + 
                            $"{savedAsset.GetStatus(),-10}"); 

                        Console.WriteLine(); 
                    }
                    break; 

                } else
                {
                    Console.WriteLine("No such ID exists, try again."); 
                    continue; 
                }
            }
        }    
    }

}

void OverviewReport()
{
    //Office asset counts 
    using (var context = new AppDbContext())
        {
            List<Asset> list = context.Assets.ToList(); 
            Dictionary<String, int> officeAssetCounts = new Dictionary<string, int>(); 

            foreach(Office office in Enum.GetValues(typeof(Office)))
            {
                int count = list.Count(delegate (Asset asset)
                {
                    return asset.Office == office; 
                });

                officeAssetCounts.Add(office.ToString(), count); 
            }

            List<Asset> assetsNearingExpiration = list.Where(delegate (Asset asset)
            {
                return asset.GetStatus() == Status.RED; 
            }).ToList(); 

            const string reportHeader = "================ REPORT =================";
            string sectionDivider = new string('-', reportHeader.Length);

            Console.WriteLine();
            Console.WriteLine(reportHeader);
            Console.WriteLine();
            Console.WriteLine("Office Asset Counts");
            Console.WriteLine(sectionDivider);
            foreach (var (office, count) in officeAssetCounts)
            {
                string label = $"{office} Office";
                Console.WriteLine($"{label,-14}: {count,2}");
            }

            Console.WriteLine();
            Console.WriteLine("Assets Near Expiration");
            Console.WriteLine(sectionDivider);
            foreach (Asset asset in assetsNearingExpiration)
            {
                Console.WriteLine($"{asset.Brand} {asset.Model}");
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', reportHeader.Length));
        }

}

void OfficeReport()
{
    using (var context = new AppDbContext())
    {
        List<Asset> assets = context.Assets.ToList(); 
        foreach(Office office in Enum.GetValues(typeof(Office)))
        {
            List<Asset> officeAssets = assets.Where(delegate (Asset asset)
            {
              return asset.Office == office;   
            }).ToList(); 

            if(officeAssets.Count > 0)
            {
                double totalValue = officeAssets.Sum(delegate (Asset asset)
                {
                    return asset.PriceUSD;
                });

                const string rowFormat = "{0,-4}{1,-11}{2,-13}{3,-18}{4}";
                string header = $"================ {office.ToString().ToUpperInvariant()} OFFICE ================";

                Console.WriteLine();
                Console.WriteLine(header);
                Console.WriteLine();
                Console.WriteLine(rowFormat, "ID", "Asset", "Brand", "Price", "Status");
                Console.WriteLine(new string('-', 54));

                foreach (Asset asset in officeAssets)
                {
                    string assetType = asset is Mobile ? "Mobile" : asset.DisplayType();
                    string price = $"{asset.PriceUSD:N2} USD";
                    Console.WriteLine(rowFormat, asset.ID, assetType, asset.Brand, price, asset.GetStatus());
                }

                Console.WriteLine();
                Console.WriteLine($"Total Office Value: {totalValue:N2} USD");
                Console.WriteLine();
                
            }
        }
    }
}
    

    





