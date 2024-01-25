using Models.Models.Domain.Base;
using Models.Interfaces;

namespace Models.Models.DTO
{
    public class AnswerDTO : AnswerBase, IIdentity
    {
        public int Id { get; set; }
    }
}
