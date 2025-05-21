var connection = new DatabaseConnection();

var userService = new UserService(connection);
try
{

    var user = userService.GetUser(-1);

    Console.WriteLine(user);
}
catch (Exception ex) { }
Console.WriteLine(connection.ConnectionOpen);

public class DatabaseConnection
{
    public bool ConnectionOpen { get; private set; }

    public void Open() => ConnectionOpen = true;

    public void Close() => ConnectionOpen = false;

    public string QueryUserById(int userId)
    {
        if (!ConnectionOpen)
            throw new InvalidOperationException("Connection must be open before");

        if (userId < 0)
            throw new ArgumentOutOfRangeException(nameof(userId));

        return $"User_{userId}";
    }
}

public class UserService
{
    private readonly DatabaseConnection _connection;

    public UserService(DatabaseConnection connection) => _connection = connection;

    public string GetUser(int userId)
    {
        string user = null;
        _connection.Open();
        try
        {
            user = _connection.QueryUserById(userId); 
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exeption occured while reading the database");
            Console.WriteLine(ex.Message);
            _connection.Close();
            throw;
        }
        finally
        {
            _connection.Close();
        }

        return user;
    }
}
