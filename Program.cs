using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Computers(
        id int not null primary key, 
        ram varchar (100) not null,
        processor varchar(100) not null
    );
";

command.ExecuteNonQuery();

connection.Close();

var ModelName = args[0];
var ModelAction = args[1];

if (ModelName == "Computer")
{
    if(ModelAction == "List")
    {
        Console.WriteLine("List Computer");
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        command= connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;"; 

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0),reader.GetString(1), reader.GetString(2));    
        }

        connection.Close();
    }

    if(ModelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];
        
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        command= connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)"; 
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery();
        connection.Close();
    }
}

