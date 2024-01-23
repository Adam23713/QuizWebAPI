using AutoMapper;
using Models.Models.DTO;
using Models.Modles.Domain;
using Models.Requests.Quiz;
using Models.Response.Quiz;

namespace Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quiz, GetQuizResponse>().ReverseMap();       
            CreateMap<Quiz, CreateQuizRequest>().ReverseMap();       
            CreateMap<Question, QuestionDTO>().ReverseMap();       
            CreateMap<Answer, AnswerDTO>().ReverseMap();       
            CreateMap<UserAnswerDTO, UserAnswer>().ReverseMap();       
        }
    }
}
