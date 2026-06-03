using Microsoft.Data.Sqlite;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence;
using System.Collections.Generic;

namespace QuizManagement.API.Infrastructure.Persistence.Repositories
{
    public class QuizRepository
    {
        // 1. GET ALL
        public List<Quiz> GetAllQuizzes()
        {
            var quizzes = new List<Quiz>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Title, Description, TimeLimit FROM Quizzes";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                quizzes.Add(new Quiz
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    TimeLimit = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                });
            }
            return quizzes;
        }

        // 2. GET BY ID
        public Quiz? GetQuizById(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Title, Description, TimeLimit FROM Quizzes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Quiz
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    TimeLimit = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                };
            }
            return null;
        }

        // 3. POST (Create)
        public void AddQuiz(Quiz quiz)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Quizzes (Title, Description, TimeLimit) VALUES (@title, @description, @timeLimit)";
            command.Parameters.AddWithValue("@title", quiz.Title);
            command.Parameters.AddWithValue("@description", (object)quiz.Description ?? System.DBNull.Value);
            command.Parameters.AddWithValue("@timeLimit", quiz.TimeLimit);
            command.ExecuteNonQuery();
        }

        // 4. PUT (Update)
        public void UpdateQuiz(Quiz quiz)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Quizzes SET Title = @title, Description = @description, TimeLimit = @timeLimit WHERE Id = @id";
            command.Parameters.AddWithValue("@title", quiz.Title);
            command.Parameters.AddWithValue("@description", (object)quiz.Description ?? System.DBNull.Value);
            command.Parameters.AddWithValue("@timeLimit", quiz.TimeLimit);
            command.Parameters.AddWithValue("@id", quiz.Id);
            command.ExecuteNonQuery();
        }

        // 5. DELETE
        public void DeleteQuiz(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();

            // Pehle related questions delete karo
            command.CommandText = "DELETE FROM Questions WHERE QuizId = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            // Phir quiz delete karo
            command.CommandText = "DELETE FROM Quizzes WHERE Id = @id";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}