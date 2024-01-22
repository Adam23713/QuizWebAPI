using AutoMapper;
using Models.Modles.Domain;
using Models.Modles.DTO;

namespace Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
        }
    }
}
