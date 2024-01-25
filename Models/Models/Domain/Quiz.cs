using Models.Models.Domain.Base;
using Models.Interfaces;

namespace Models.Models.Domain
{
    public class Quiz : QuizeBase<Question>, IIdentity
    {
        public int Id { get; set; }
    }
}
