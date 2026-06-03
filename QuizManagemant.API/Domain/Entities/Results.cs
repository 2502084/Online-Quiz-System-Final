namespace QuizManagement.API.Domain.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int Score { get; set; }
        public string DateObtained { get; set; } = string.Empty;
    }
}