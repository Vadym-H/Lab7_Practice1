using ReflectionTutorial;
using System.Reflection;

//LecturePart();
//Task1();
//Task2();
//Task3();
//Task4();
static void LecturePart()
{
    var data = File.ReadAllText("car.txt").Split(';');

    var brand = data[0];
    var model = data[1];
    var tankCapacity = int.Parse(data[2]);
    var fuelConsumption = double.Parse(data[3]);
    var fuelLevel = int.Parse(data[4]);
    var odometer = int.Parse(data[5]);

    var car = new Car(brand, model, tankCapacity, fuelConsumption);

    var carType = typeof(Car);
    var fuelLevelField = carType.GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
    var odometerField = carType.GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);

    fuelLevelField.SetValue(car, fuelLevel);
    odometerField.SetValue(car, odometer);

    Console.WriteLine($"{car.Brand} {car.Model} drove {car.Odometer} " +
        $"kilometers and remaining fuel is {car.FuelLevel}");
}
static void Task1()
{
    var car = new Car("Toyota", "Corolla", 50, 6.0);
    Console.WriteLine($"Cars made so far {Car.CarsMade}");

    car.AddFuel(20);
    car.AddFuel(70);

    car.Drive(100);
    car.Drive(200);

    var fuelLevelField = typeof(Car).GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
    var odometerField = typeof(Car).GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);

    var fuelLevel = fuelLevelField.GetValue(car);
    var odometer = odometerField.GetValue(car);

    Console.WriteLine($"{fuelLevel}  {odometer}");


    File.WriteAllText("car.txt", $"{car.Brand};{car.Model};{car.TankCapacity};{car.FuelConsumption};{fuelLevel};{odometer}");
    Console.WriteLine($"{car.Brand} {car.Model} drove {car.Odometer} " +
        $"kilometers and remaining fuel is {car.FuelLevel}");
}

static void Task2()
{
    var cars = new List<Car>
{
    new Car("Toyota", "Corolla", 50, 6.0),
    new Car("Honda", "Civic", 45, 5.8),
    new Car("Ford", "Focus", 55, 6.2)
};


    Console.WriteLine($"Cars made so far {Car.CarsMade}");

    var carsMadeField = typeof(Car).GetProperty("CarsMade", BindingFlags.Public | BindingFlags.Static);
    carsMadeField.SetValue(null, 0);
    Console.WriteLine($"Cars made after reflaction {Car.CarsMade}");
}



static void Task3()
{
    var constructors = typeof(Car).GetConstructors(BindingFlags.Public | BindingFlags.Instance);

    foreach (var constructor in constructors)
    {
        var parameters = constructor.GetParameters();
        foreach (var parameter in parameters)
        {
            Console.WriteLine($"{parameter.ParameterType.Name}: {parameter.Name}");
        }
    }
}



static void Task4()
{
    var data = File.ReadAllText("car.txt").Split(';');

    var brand = data[0];
    var model = data[1];
    var tankCapacity = int.Parse(data[2]);
    var fuelConsumption = double.Parse(data[3]);
    var fuelLevel = int.Parse(data[4]);
    var odometer = int.Parse(data[5]);

    var car = (Car)Activator.CreateInstance(typeof(Car), brand, model, tankCapacity, fuelConsumption);

    var carType = typeof(Car);
    var fuelLevelField = carType.GetField("_fuelLevel", BindingFlags.NonPublic | BindingFlags.Instance);
    var odometerField = carType.GetField("_odometer", BindingFlags.NonPublic | BindingFlags.Instance);

    fuelLevelField.SetValue(car, fuelLevel);
    odometerField.SetValue(car, odometer);

    Console.WriteLine($"{car.Brand} {car.Model} drove {car.Odometer} " +
        $"kilometers and remaining fuel is {car.FuelLevel}");
}