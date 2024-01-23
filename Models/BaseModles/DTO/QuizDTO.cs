using Models.BaseModles.Domain.Base;
using Models.Modles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModles.DTO
{
    /// <summary>
    /// Quizz DTO. No ids in the objects
    /// </summary>
    public class QuizDTO : QuizeBase
    {
        public List<QuestionDTO> Questions { get; set; } = null!;
    }
}
