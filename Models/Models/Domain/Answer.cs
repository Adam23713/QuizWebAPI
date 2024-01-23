using Models.BaseModles.Domain.Base;
using Models.Interfaces;

namespace Models.Modles.Domain
{
    public class Answer : AnswerBase, IIdentity
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
    }
}
