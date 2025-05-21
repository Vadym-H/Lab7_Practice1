using ReflectionTutorial;
using System.Collections.Concurrent;
using System.Reflection;

var addFuelMethod = typeof(Car).GetMethod("AddFuel", BindingFlags.Public | BindingFlags.Instance);

Console.WriteLine(addFuelMethod.MemberType);
Console.WriteLine(addFuelMethod.ReturnParameter);
Console.WriteLine();
foreach (var parameter in addFuelMethod.GetParameters())
{
    Console.WriteLine(parameter.Name);
    Console.WriteLine(parameter.ParameterType);
}
