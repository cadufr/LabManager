using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var DatabaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(DatabaseConfig);

var computerRepository = new ComputerRepository(DatabaseConfig);


// Routing
var modelName = args[0];
var modelAction = args[1];


if (modelName == "Computer")
{
    if (modelAction == "List")
    {
            var connection = new SqliteConnection("Data Source=database.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Computers;";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    "{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2)
                    );
            }

            reader.Close();
            connection.Close(); 

    }
    if (modelAction == "New")
    {
            int id = Convert.ToInt32(args[2]);
            string ram = args[3];
            string processor = args[4];

            var computer = new Computer(id: id, ram: ram, processor: processor);
            computerRepository.Save(computer);
    }
}

if (modelName == "Lab")
{
    if (modelAction == "List")
    {
        Console.WriteLine("ComputerList");
        foreach (var computer in computerRepository.GetALL())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor
            );
        }
    }
    if (modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        int number = Convert.ToInt32(args[3]);
        string name = args[4];
        string block = args[5];

        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Lab VALUES($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);
        command.ExecuteNonQuery();
        connection.Close();
    }
}
