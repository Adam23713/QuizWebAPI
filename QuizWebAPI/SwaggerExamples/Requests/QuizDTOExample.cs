using Models.Modles.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace QuizWebAPI.SwaggerExamples.Requests
{
    /// <summary>
    /// Example for a quiz dto (to swagger GUI)
    /// </summary>
    public class QuizDTOExample : IExamplesProvider<QuizDTO>
    {
        public QuizDTO GetExamples()
        {
            return new QuizDTO
            {
                IsEnabled = true,
                Name = "Test Quiz",
                Description = "This is a simple test quiz",
                TimeLimitInMinutes = 120,
                Begin = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                Questions = new List<QuestionDTO> {
                    new QuestionDTO {
                        QuestionText = "Is 1 odd?",
                        Answers = new List<AnswerDTO> {
                            new AnswerDTO { IsRight = true, AnswerText = "Yes" },
                            new AnswerDTO { IsRight = false, AnswerText = "No" },
                            new AnswerDTO { IsRight = false, AnswerText = "Maybe" },
                            new AnswerDTO { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    },
                    new QuestionDTO {
                        QuestionText = "Is 2 odd?",
                        Answers = new List<AnswerDTO> {
                            new AnswerDTO { IsRight = false, AnswerText = "Yes" },
                            new AnswerDTO { IsRight = true, AnswerText = "No" },
                            new AnswerDTO { IsRight = false, AnswerText = "Maybe" },
                            new AnswerDTO { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    },
                    new QuestionDTO {
                        QuestionText = "Is 2 even?",
                        Answers = new List<AnswerDTO> {
                            new AnswerDTO { IsRight = true, AnswerText = "Yes" },
                            new AnswerDTO { IsRight = false, AnswerText = "No" },
                            new AnswerDTO { IsRight = false, AnswerText = "Maybe" },
                            new AnswerDTO { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    }
                }
            };
        }
    }
}
