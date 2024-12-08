using Npgsql;
// using System;

namespace api;

class Program
{
    static void GetBlogs()
    {
        string connectionString = "Host=your_host;Username=your_username;Password=your_password;Database=your_database";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Id, Title FROM Blogs";

            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Title: {reader["Title"]}");
                    }
                }
            }
        }
    }
}
