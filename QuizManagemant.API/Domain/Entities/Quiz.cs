namespace QuizManagement.API.Domain.Entities
{
    public class Quiz
    {
     
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int TimeLimit { get; set; }
    }
}