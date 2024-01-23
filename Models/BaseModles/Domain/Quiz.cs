using Models.BaseModles.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class Quiz : QuizeBase
    {
        [Key]
        public int Id { get; set; }

        public List<Question> Questions { get; set; } = null!;
    }
}
