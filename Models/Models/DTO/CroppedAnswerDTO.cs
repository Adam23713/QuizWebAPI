using Models.Interfaces;
using Models.Models.Domain.Base;

namespace Models.Models.DTO
{
    public class CroppedAnswerDTO : CroppedAnswer, IIdentity
    {
        public int Id { get; set; }
    }
}
