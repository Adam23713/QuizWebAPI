using Models.Models.Domain.Base;
using Models.Interfaces;

namespace Models.Models.Domain
{
    public class Question : QuestionBase<Answer>, IIdentity
    {
        public int Id { get; set; }

        public int QuizId { get; set; }
    }
}
