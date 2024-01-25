using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models.Domain;
using Models.Requests.Quiz;

namespace DataAccess.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly SQLApplicationContext context;

        public QuizRepository(SQLApplicationContext context, ILogger<QuizRepository> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            context.Quizzes.Add(quiz);
            await context.SaveChangesAsync();
            return quiz;
        }

        public async Task<bool> DeleteQuizAsync(Quiz quiz)
        {
            if(quiz != null)
            {
                var result = context.Quizzes.Remove(quiz);
                if(result != null && result.State == EntityState.Deleted)
                {
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            return await context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).FirstOrDefaultAsync(q => q.Id == id);
        }

        public IEnumerable<Quiz> GetQuizzes()
        {
            return context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).ToList();
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            return await context.Quizzes.Include(q => q.Questions).ThenInclude(a => a.Answers).ToListAsync();
        }

        public async Task<bool> UpdateQuizAsync(UpdateQuizRequest updatedQuiz, Quiz original)
        {
            UpdateProperties(updatedQuiz, original);
            context.Update(original);
            var res = await context.SaveChangesAsync();
            return (res > 0);
        }

        private void UpdateProperties(UpdateQuizRequest other, Quiz quiz)
        {
            quiz.IsEnabled = other.IsEnabled;
            quiz.Name = other.Name;
            quiz.Description = other.Description;
            quiz.TimeLimitInMinutes = other.TimeLimitInMinutes;
            quiz.Begin = other.Begin;
            quiz.End = other.End;

            //Update, create and delete questions
            List<int> deletedQuestions = new List<int>();
            var updatedOldQuestions = other.Questions.Where(q => q.Id != null);
            var newQuestions = other.Questions.Where(q => q.Id == null);
            UpdateQuestions(updatedOldQuestions, quiz.Questions, deletedQuestions);
            deletedQuestions.ForEach(id =>
            {
                var item = quiz.Questions.FirstOrDefault(item => item.Id == id);
                if(item != null)
                {
                    context.Questions.Remove(item);
                }
            });
            quiz.Questions.AddRange(mapper.Map<List<Question>>(newQuestions));
        }

        private void UpdateQuestions(IEnumerable<QuestionWitNullableID> updatedOldQuestions, IEnumerable<Question> source, List<int> deletedQuestions)
        {
            foreach (var originalQuestion in source)
            {
                var updatedQuestion = updatedOldQuestions.FirstOrDefault(q => q.Id == originalQuestion.Id);
                if(updatedQuestion != null)
                {
                    originalQuestion.QuestionText = updatedQuestion.QuestionText;

                    //Update and create Answers
                    List<int> deletedAnswers = new List<int>();
                    var updatedOldAnswer = updatedQuestion.Answers.Where(q => q.Id != null);
                    var newAnswers= updatedQuestion.Answers.Where(q => q.Id == null);
                    UpdateAnswers(updatedOldAnswer, originalQuestion.Answers, deletedAnswers);

                    deletedAnswers.ForEach(id =>
                    {
                        var item = originalQuestion.Answers.FirstOrDefault(item => item.Id == id);
                        if (item != null)
                        {
                            context.Answers.Remove(item);
                        }
                    });
                    originalQuestion.Answers.AddRange(mapper.Map<List<Answer>>(newAnswers));
                }
                else
                {
                    deletedQuestions.Add(originalQuestion.Id);
                }
            }
        }

        private void UpdateAnswers(IEnumerable<AnswerWithNullableID> updatedOldAnswer, List<Answer> answers, List<int> deletedAnswers)
        {
            foreach(var answer in answers)
            {
                var updatedAnswer = updatedOldAnswer.FirstOrDefault(q => q.Id == answer.Id);
                if(updatedAnswer != null)
                {
                    answer.IsRight = updatedAnswer.IsRight;
                    answer.AnswerText = updatedAnswer.AnswerText;
                }
                else
                {
                    deletedAnswers.Add(answer.Id);
                }
            }
        }
    }
}
