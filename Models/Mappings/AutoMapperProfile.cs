using AutoMapper;
using Models.BaseModles.DTO;
using Models.BaseModles.DTO.Requests.Quiz;
using Models.BaseModles.DTO.Response.Quiz;
using Models.Modles.Domain;

namespace Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quiz, QuizDTO>().ReverseMap();
            CreateMap<Quiz, GetQuizResponse>().ReverseMap();
            CreateMap<CreateQuizRequest, QuizDTO>().ReverseMap();

            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<QuestionWithId, QuestionDTO>().ReverseMap();
            CreateMap<QuestionWithId, Question>().ReverseMap();


            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<AnswersWithId, AnswerDTO>().ReverseMap();
            CreateMap<AnswersWithId, Answer>().ReverseMap();        
        }
    }
}
