using Models.Models.Domain.Base;
using Models.Interfaces;

namespace Models.Models.Domain
{
    public class UserAnswer : UserAnswerBase, IIdentity
    {
        public int Id { get; set; }
    }
}
