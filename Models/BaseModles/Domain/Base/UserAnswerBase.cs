using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModles.Domain.Base
{
    /// <summary>
    /// Base model of User Answer. No id in the base class
    /// </summary>
    public class UserAnswerBase
    {
        public int UserId { get; set; }

        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        List<int> Answers { get; set; } = null!;
    }
}
