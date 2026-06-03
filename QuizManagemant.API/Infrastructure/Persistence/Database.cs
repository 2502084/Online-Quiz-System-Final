using Microsoft.Data.Sqlite;

namespace QuizManagement.API.Infrastructure.Persistence
{
    public static class Database
    {

        public static string ConnectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuizManagement.db")}";

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}