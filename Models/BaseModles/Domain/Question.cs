using Models.BaseModles.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Modles.Domain
{
    public class Question : QuestionBase
    {
        [Key]
        public int Id { get; set; }

        public int QuizId { get; set; }

        public List<Answer> Answers { get; set; } = null!;
    }
}
