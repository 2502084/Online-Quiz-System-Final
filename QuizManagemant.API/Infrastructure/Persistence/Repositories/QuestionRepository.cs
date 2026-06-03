using Microsoft.Data.Sqlite;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence;
using System.Collections.Generic;

namespace QuizManagement.API.Infrastructure.Persistence.Repositories
{
    public class QuestionRepository
    {
        // 1. GET ALL
        public List<Question> GetAllQuestions()
        {
            var questions = new List<Question>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Questions";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                questions.Add(new Question
                {
                    Id = reader.GetInt32(0),
                    QuizId = reader.GetInt32(1),
                    QuestionText = reader.GetString(2),
                    OptionA = reader.GetString(3),
                    OptionB = reader.GetString(4),
                    OptionC = reader.GetString(5),
                    OptionD = reader.GetString(6),
                    CorrectAnswer = reader.GetString(7)
                });
            }
            return questions;
        }

        // 2. GET BY ID (Naya Add Kiya)
        public Question? GetQuestionById(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Questions WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Question
                {
                    Id = reader.GetInt32(0),
                    QuizId = reader.GetInt32(1),
                    QuestionText = reader.GetString(2),
                    OptionA = reader.GetString(3),
                    OptionB = reader.GetString(4),
                    OptionC = reader.GetString(5),
                    OptionD = reader.GetString(6),
                    CorrectAnswer = reader.GetString(7)
                };
            }
            return null;
        }

        // 3. POST (Create)
        public void AddQuestion(Question question)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Questions (QuizId, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) VALUES (@quizId, @text, @a, @b, @c, @d, @correct)";
            command.Parameters.AddWithValue("@quizId", question.QuizId);
            command.Parameters.AddWithValue("@text", question.QuestionText);
            command.Parameters.AddWithValue("@a", question.OptionA);
            command.Parameters.AddWithValue("@b", question.OptionB);
            command.Parameters.AddWithValue("@c", question.OptionC);
            command.Parameters.AddWithValue("@d", question.OptionD);
            command.Parameters.AddWithValue("@correct", question.CorrectAnswer);
            command.ExecuteNonQuery();
        }

        // 4. PUT (Update)
        public void UpdateQuestion(Question question)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Questions SET QuizId = @quizId, QuestionText = @text, OptionA = @a, OptionB = @b, OptionC = @c, OptionD = @d, CorrectAnswer = @correct WHERE Id = @id";
            command.Parameters.AddWithValue("@quizId", question.QuizId);
            command.Parameters.AddWithValue("@text", question.QuestionText);
            command.Parameters.AddWithValue("@a", question.OptionA);
            command.Parameters.AddWithValue("@b", question.OptionB);
            command.Parameters.AddWithValue("@c", question.OptionC);
            command.Parameters.AddWithValue("@d", question.OptionD);
            command.Parameters.AddWithValue("@correct", question.CorrectAnswer);
            command.Parameters.AddWithValue("@id", question.Id);
            command.ExecuteNonQuery();
        }

        // 5. DELETE
        public void DeleteQuestion(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Questions WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}