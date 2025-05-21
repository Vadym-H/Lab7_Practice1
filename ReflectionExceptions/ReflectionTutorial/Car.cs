using System.Security.Cryptography.X509Certificates;

namespace ReflectionTutorial;

public class Car
{
    public static int CarsMade { get; set; }

    public string Brand { get; set; }
    public string Model { get; set; }
    public int TankCapacity { get; set; }
    public double FuelConsumption { get; set; }
    public int FuelLevel => (int)_fuelLevel;
    public int Odometer => (int)_odometer;

    private double _fuelLevel;
    private double _odometer;

    public Car(string brand, string model, int tankCapacity, double fuelConsumption)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brand, nameof(brand));
        ArgumentException.ThrowIfNullOrWhiteSpace(model, nameof(model));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(tankCapacity, 0, nameof(tankCapacity));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(fuelConsumption, 0, nameof(fuelConsumption));
        Brand = brand;
        Model = model;
        TankCapacity = tankCapacity;
        FuelConsumption = fuelConsumption;

        CarsMade++;
    }
    public class FuelOverflowExcepion : Exception
    {
        public double AmountAdded { get; }
        public double CurrentFuelLevel { get; }
        public double ExcessAmount { get; }
        public FuelOverflowExcepion(double amountAdded, double currentFuelLevel, double tankCapacity) 
            : base($"Cannot add {amountAdded}L of fuel to a tank that already contains {currentFuelLevel}L. " +
                  $"This would exceed the capacity of {tankCapacity}L by {amountAdded + currentFuelLevel - tankCapacity}L") 
        {
            AmountAdded = amountAdded;
            CurrentFuelLevel = currentFuelLevel;
            ExcessAmount = amountAdded + currentFuelLevel - tankCapacity;
        }
    }

    public void AddFuel(double amount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0, nameof(amount));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(amount, TankCapacity, nameof(amount));

        if(_fuelLevel +  amount > TankCapacity) 
            throw new FuelOverflowExcepion(amount, _fuelLevel, TankCapacity);

            _fuelLevel += amount;
    }
    public class FuelIsNotEnoughToDriveException : Exception
    {
        public double Distance { get; }
        public double FuelConsumption { get; }
        public double FuelLevel { get; }
        public double FuelNeeded { get; }
        public FuelIsNotEnoughToDriveException(double distance, double fuelNeeded, double fuelLevel, double fuelConsumption) 
            : base($"Cannot drive {distance}km, because current fuel level is {fuelLevel}L while {fuelNeeded}L is needed.")
        {
            Distance = distance;
            FuelConsumption = fuelConsumption;
            FuelLevel = fuelLevel;
            FuelNeeded = fuelNeeded;
        }
    }

    public void Drive(double distance)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(distance, 0, nameof(distance));

        var fuelNeeded = distance / 100.0 * FuelConsumption;

        if (fuelNeeded > _fuelLevel)
            throw new FuelIsNotEnoughToDriveException(distance, fuelNeeded, _fuelLevel, FuelConsumption);
        else
        {
            _fuelLevel -= fuelNeeded;
            _odometer += distance;
        }
    }
}