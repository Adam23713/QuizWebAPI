using Models.BaseModles.Domain.Base;
using Models.Modles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModles.DTO
{
    public class QuestionDTO : QuestionBase
    {
        public List<AnswerDTO> Answers { get; set; } = null!;
    }
}
