using Models.Models.Domain.Base;
using Models.Interfaces;

namespace Models.Models.Domain
{
    public class Answer : AnswerBase, IIdentity
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
    }
}
