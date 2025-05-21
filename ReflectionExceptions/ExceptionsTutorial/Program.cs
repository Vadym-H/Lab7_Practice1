Console.WriteLine("Welcome to the numbers adder program. This program will add two integers for you");

Console.WriteLine("Enter the first number");
var first = Console.ReadLine();

Console.WriteLine("Enter the second number");
var second = Console.ReadLine();

try
{
    var f = int.Parse(first);
    var s = int.Parse(second);

    var result = f + s;

    Console.WriteLine($"The result is {result}");
}
catch( FormatException ex )
{
    Console.WriteLine(ex.Message);
}
catch
{
    Console.WriteLine("value out of range");
}