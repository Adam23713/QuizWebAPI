using Models.Models.Domain.Base;
using Models.Requests.Quiz;
using Swashbuckle.AspNetCore.Filters;

namespace QuizWebAPI.SwaggerExamples.Requests
{
    /// <summary>
    /// Example for a quiz dto (to swagger GUI)
    /// </summary>
    public class CreateQuizExample : IExamplesProvider<CreateQuizRequest>
    {
        public CreateQuizRequest GetExamples()
        {
            return new CreateQuizRequest
            {
                IsEnabled = true,
                Name = "Test Quiz",
                Description = "This is a simple test quiz",
                TimeLimitInMinutes = 120,
                Begin = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                Questions = new List<QuestionBase<AnswerBase>> 
                {
                    new()
                    {
                        QuestionText = "Is 1 odd?",
                        Answers = new List<AnswerBase> {
                            new() { IsRight = true, AnswerText = "Yes" },
                            new() { IsRight = false, AnswerText = "No" },
                            new() { IsRight = false, AnswerText = "Maybe" },
                            new() { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    },
                    new()
                    {
                        QuestionText = "Is 2 odd?",
                        Answers = new List<AnswerBase> {
                            new() { IsRight = false, AnswerText = "Yes" },
                            new() { IsRight = true, AnswerText = "No" },
                            new() { IsRight = false, AnswerText = "Maybe" },
                            new() { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    },
                    new() {
                        QuestionText = "Is 2 even?",
                        Answers = new List<AnswerBase> {
                            new() { IsRight = true, AnswerText = "Yes" },
                            new() { IsRight = false, AnswerText = "No" },
                            new() { IsRight = false, AnswerText = "Maybe" },
                            new() { IsRight = false, AnswerText = "Sometimes yes" },
                        }
                    }
                }
            };
        }
    }
}
