using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTO;
using Models.Response.Quiz;
using QuizUI.Models;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.Controllers
{
    /// <summary>
    /// This controller manage the game
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    [EnableCors]
    //[Authorize(Roles = "Admin, Gamer")]
    public class GameController : Controller
    {

        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IQuizService quizService;

        public GameController(ILogger<QuizController> logger, IMapper mapper, IQuizService quizService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.quizService = quizService;
        }

        [HttpGet]
        [Route("result/{id:int}")]
        public async Task<IActionResult> ShowQuizResult(int id, [FromQuery] int userId)
        {
            return Ok(await quizService.GetResultAsync(id, userId));
        }

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitAnswer([FromBody] UserAnswerDTO userAnswer)
        {
            await quizService.SubmitAnswer(userAnswer);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Start(int id, [FromQuery] int userId)
        {
            var quiz = await quizService.GetQuizByIdAsync(id);
            if(quiz != null)
            {
                var response = mapper.Map<GetQuizForGameResponse>(quiz);
                response.UserId = userId;
                response.ApiURL = $"{Request.Scheme}://{Request.Host}/game";

                //Submit Start
                response.BeginQuestionIndex = await quizService.SubmitQuizStart(id, userId, response.TimeLimitInMinutes);

                
                return Ok(response);
            }
            return NotFound($"Quiz not found: {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetQuizzesInfoAsync()
        {
            List<QuizInfoDTO> infos = new List<QuizInfoDTO>();
            var quizzes = await quizService.GetQuizzesAsync();
            if(quizzes != null)
            {
                foreach (var quiz in quizzes)
                {
                    infos.Add(new QuizInfoDTO 
                    {
                        Id = quiz.Id,
                        QuizName = quiz.Name,
                        Description = quiz.Description
                    });
                }
            }
            return Ok(infos);
        }
    }
}
