using Models.BaseModles.Domain.Base;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class UserAnswer : UserAnswerBase, IIdentity
    {
        public int Id { get; set; }
    }
}
