using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Domain
{
    public class Question
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string QuestionText { get; set; } = null!;

        public List<Answer> Answers { get; set; } = null!;
    }
}
