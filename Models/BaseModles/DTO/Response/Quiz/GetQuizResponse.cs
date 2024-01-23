using Models.BaseModles.Domain.Base;

namespace Models.BaseModles.DTO.Response.Quiz
{
    public class GetQuizResponse : QuizeBase
    {
        public int Id { get; set; }

        public List<QuestionWithId> Questions { get; set; } = null!;
    }

    public class QuestionWithId : QuestionBase 
    {
        public int Id { get; set; }

        public List<AnswersWithId> Answers { get; set; } = null!;
    }

    public class AnswersWithId : AnswerBase 
    {
        public int Id { get; set; }
    }

}
