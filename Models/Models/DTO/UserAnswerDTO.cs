namespace Models.Models.DTO
{
    public class UserAnswerDTO
    {
        public int UserId { get; set; }

        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        public List<int> AnswersList { get; set; } = null!;
    }
}
