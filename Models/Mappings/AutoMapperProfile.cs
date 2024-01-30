using AutoMapper;
using Models.Models.DTO;
using Models.Models.Domain;
using Models.Requests.Quiz;
using Models.Response.Quiz;
using Models.Models.Domain.Base;

namespace Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quiz, GetQuizResponse>().ReverseMap();       
            CreateMap<Quiz, CreateQuizRequest>().ReverseMap();       
            CreateMap<Quiz, GetQuizForGameResponse>().ReverseMap(); 
            CreateMap<QuestionForGame, Question>().ReverseMap(); 
            CreateMap<Answer, CroppedAnswerDTO>().ReverseMap(); 
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<AnswerBase, Answer>().ReverseMap();
            CreateMap<AnswerWithNullableID, AnswerBase>().ReverseMap();
            CreateMap<QuestionWitNullableID, Question>().ReverseMap();      
        }
    }
}
