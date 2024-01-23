using Models.BaseModles.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class Answer : AnswerBase
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }
    }
}
