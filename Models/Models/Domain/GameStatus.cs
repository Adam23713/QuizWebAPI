namespace Models.Models.Domain
{
    public class GameStatus
    {
        public int QuizId { get; set; }

        public int UserId { get; set; }

        public uint Points { get; set; }

        public Dictionary<int, IList<int>> QuestionWithAnswers { get; set; } = new Dictionary<int, IList<int>>();

        public DateTime Started { get; set; }

        public DateTime WillEndAt { get; set; }
    }
}
