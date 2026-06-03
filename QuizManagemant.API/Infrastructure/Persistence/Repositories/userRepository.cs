using Microsoft.Data.Sqlite;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence;
using System.Collections.Generic;

namespace QuizManagement.API.Infrastructure.Persistence.Repositories
{
    public class UserRepository
    {
        // 1. GET ALL
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3)
                });
            }
            return users;
        }

        // 2. GET BY ID 
        public User? GetUserById(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3)
                };
            }
            return null;
        }

        // 3. POST 
        public void AddUser(User user)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Name, Email, Password) VALUES (@name, @email, @password)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.ExecuteNonQuery();
        }

        // 4. PUT 
        public void UpdateUser(User user)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Users SET Name = @name, Email = @email, Password = @password WHERE Id = @id";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@id", user.Id);
            command.ExecuteNonQuery();
        }

        // 5. DELETE 
        public void DeleteUser(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}