namespace Models.Modles.DTO
{
    public class QuestionDTO
    {
        public string QuestionText { get; set; } = null!;

        public List<AnswerDTO> Answers { get; set; } = null!;
    }
}
