using Models.BaseModles.Domain.Base;
using Models.Interfaces;
using Models.Models.DTO;

namespace Models.Response.Quiz
{
    public class GetQuizResponse : QuizeBase<QuestionDTO>, IIdentity
    {
        public int Id { get; set; }
    }

}
