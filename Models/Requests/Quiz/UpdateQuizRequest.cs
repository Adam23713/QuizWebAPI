using Models.Interfaces;
using Models.Models.Domain.Base;

namespace Models.Requests.Quiz
{
    public class UpdateQuizRequest : QuizeBase<QuestionWitNullableID>, IIdentity
    {
        public int Id { get; set; }
    }

    public class QuestionWitNullableID : QuestionBase<AnswerWithNullableID>
    {
        public int? Id { get; set; }

        public int QuizId { get; set; }
    }

    public class AnswerWithNullableID : AnswerBase
    {
        public int? Id { get; set; }

        public int QuestionId { get; set; }
    }
}
