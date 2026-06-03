using Microsoft.Data.Sqlite;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence;
using System.Collections.Generic;

namespace QuizManagement.API.Infrastructure.Persistence.Repositories
{
    public class ResultRepository
    {
        // 1. GET ALL 
        public List<Result> GetAllResults()
        {
            var results = new List<Result>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Results";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new Result
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    QuizId = reader.GetInt32(2),
                    Score = reader.GetInt32(3),
                    DateObtained = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }
            return results;
        }

        // 2. GET BY ID 
        public Result? GetResultById(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Results WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Result
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    QuizId = reader.GetInt32(2),
                    Score = reader.GetInt32(3),
                    DateObtained = reader.IsDBNull(4) ? "" : reader.GetString(4)
                };
            }
            return null;
        }

        // 3. POST 
        public void AddResult(Result result)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Results (UserId, QuizId, Score, DateObtained) VALUES (@userId, @quizId, @score, @dateObtained)";
            command.Parameters.AddWithValue("@userId", result.UserId);
            command.Parameters.AddWithValue("@quizId", result.QuizId);
            command.Parameters.AddWithValue("@score", result.Score);
            command.Parameters.AddWithValue("@dateObtained",
                string.IsNullOrEmpty(result.DateObtained)
                ? DateTime.Now.ToString("yyyy-MM-dd")
                : result.DateObtained);
            command.ExecuteNonQuery();
        }

        // 4. PUT 
        public void UpdateResult(Result result)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Results SET UserId = @userId, QuizId = @quizId, Score = @score, DateObtained = @dateObtained WHERE Id = @id";
            command.Parameters.AddWithValue("@userId", result.UserId);
            command.Parameters.AddWithValue("@quizId", result.QuizId);
            command.Parameters.AddWithValue("@score", result.Score);
            command.Parameters.AddWithValue("@dateObtained",
                string.IsNullOrEmpty(result.DateObtained)
                ? DateTime.Now.ToString("yyyy-MM-dd")
                : result.DateObtained);
            command.Parameters.AddWithValue("@id", result.Id);
            command.ExecuteNonQuery();
        }

        // 5. DELETE 
        public void DeleteResult(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Results WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}