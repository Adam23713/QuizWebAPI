using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Domain
{
    public class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public bool IsRight { get; set; }

        public string AnswerText { get; set; } = null!;
    }
}
