using Models.BaseModles.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class UserAnswer : UserAnswerBase
    {
        [Key]
        public int Id { get; set; }
    }
}
