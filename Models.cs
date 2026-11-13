public abstract class Asset {
    public int ID { get; private set; }
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
    }

    public abstract String DisplayType(); 

}

public enum Office
{
    Sweden = 1,
    USA = 2, 
    Turkey = 3
}

class Computer : Asset
{
    public ComputerAsset assetType { get; set; }
    public Computer(String brand, String model, DateTime purchaseDate, double priceUSD, Office office, ComputerAsset assetType)
        : base(brand, model, purchaseDate, priceUSD, office)
    {
        this.assetType = assetType; 
    }

// Fix this later 
    public override String DisplayType()
    {
        return assetType.ToString(); 
    }


}

class Mobile : Asset 
{
    public MobileAsset assetType { get; set; }
    public Mobile(String brand, String model, DateTime purchaseDate, double priceUSD, Office office, MobileAsset assetType)
        : base(brand, model, purchaseDate, priceUSD, office)
    {
        this.assetType = assetType; 
    }

    public override string DisplayType()
    {
        return assetType.ToString();
    }

}

enum ComputerAsset
{
    Laptop, 
    Desktop
}

enum MobileAsset
{
    Phone, 
    Tablet
}