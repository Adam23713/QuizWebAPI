namespace Models.Modles.DTO
{
    public class QuizDTO
    {
        public bool IsEnabled { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ulong TimeLimitInMinutes { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public List<QuestionDTO> Questions { get; set; } = null!;
    }
}
