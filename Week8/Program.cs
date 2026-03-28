using System.Runtime.Intrinsics.Wasm;
using System.Security.Cryptography.X509Certificates;

public abstract class DeliveryItem
{
    public string TrackingNumber{get; set;}
    public double Weight{get; set;}
    public DeliveryItem(string trackingnumber, double weight)
    {
        TrackingNumber = trackingnumber;
        Weight = weight;
    }
    public abstract double CalculateCost();
    public virtual void PrintInfo()
    {
        Console.WriteLine ($"Track-Number: {TrackingNumber}, Weight: {Weight}");
    }
}

public class Letter: DeliveryItem
{
    public Letter(string trackingnumber, double weight) : base(trackingnumber, weight)
    {
    }
    public override double CalculateCost()
    {
        return (15+Weight*10);
    }
}
public class Parcel:DeliveryItem
{
    public string Dimensions{get; set;}
    public Parcel(string trackingnumber, double weight, string dimensions) : base(trackingnumber, weight)
    {
        Dimensions=dimensions;
    }
    public override double CalculateCost()
    {
        return (50 + 25*Weight);
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Dimensions:{Dimensions}");
    }
}
public class CargoContainer<T> where T : DeliveryItem
{
    private List<T> Items = new List<T>();
    public void AddItem(T item)
    {
        Items.Add(item);
    }
    public double GetTotalCost()
    {
        double s = 0;
        foreach(T item in Items)
        {
            s+=item.CalculateCost();
        }
        return s;
    }
}

public class Program
{
    public static void Main()
    {
        Letter letter1 = new Letter("1488L", 0.67);
        Parcel parcel1 = new Parcel("6769P",6.7, "52 67 69");
        letter1.PrintInfo();
        parcel1.PrintInfo();
        CargoContainer<DeliveryItem> myCargo = new CargoContainer<DeliveryItem>();
        myCargo.AddItem(letter1);
        myCargo.AddItem(parcel1);
        Console.WriteLine($"Total Cost: {myCargo.GetTotalCost()}");

    }

}