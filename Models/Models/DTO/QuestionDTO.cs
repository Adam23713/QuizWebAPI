using Models.BaseModles.Domain.Base;
using Models.Interfaces;

namespace Models.Models.DTO
{
    public class QuestionDTO : QuestionBase<AnswerDTO>, IIdentity
    {
        public int Id { get; set; }
    }
}
