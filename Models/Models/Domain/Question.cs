using Models.BaseModles.Domain.Base;
using Models.Interfaces;

namespace Models.Modles.Domain
{
    public class Question : QuestionBase<Answer>, IIdentity
    {
        public int Id { get; set; }

        public int QuizId { get; set; }
    }
}
