using AutoMapper;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Modles.Domain;
using Models.Requests.Quiz;
using Models.Response.Quiz;
using QuizWebAPI.Services.Interfaces;

namespace QuizWebAPI.Controllers
{

    /// <summary>
    /// This controller manage quiz in the system
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    [Authorize]
    public class QuizController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IQuizService quizService;

        public QuizController(ILogger<QuizController> logger, IMapper mapper, IQuizService quizService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.quizService = quizService;
        }

        /// <summary>
        /// Returns all Quizzes in the system
        /// </summary>
        /// <remarks>
        ///    Sample **request**:
        ///    
        ///        GET /Quiz/
        /// </remarks>
        /// <response code="200">Returns all the Quizzes in the system</response>
        [HttpGet]
        public async Task<IActionResult> GetAllQuiz()
        {
            var quizzes = await quizService.GetQuizzesAsync();
            return Ok(mapper.Map<List<GetQuizResponse>>(quizzes));
        }

        /// <summary>
        /// Returns the selected quiz
        /// </summary>
        /// <remarks>
        ///    Sample **request**:
        ///    
        ///        GET /Quiz/1
        /// </remarks>
        /// <response code="200">Returns the selected quiz</response>
        /// <response code="404">Quiz not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetQuizResponse), 201)]
        public async Task<IActionResult> GetQuiz([FromRoute] int id)
        {
            var quiz = await quizService.GetQuizByIdAsync(id);
            if(quiz == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<GetQuizResponse>(quiz));
        }

        /// <summary>
        /// Create a quiz in the system
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="201">Create a quiz in the system</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetQuizResponse), 201)]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest quiz)
        {
            // Convert DTO to Domain Modell
            var newQuiz = mapper.Map<Quiz>(quiz);

            var savedQuiz = await quizService.CreateQuizAsync(newQuiz);
            if (savedQuiz == null)
            {
                return BadRequest();
            }

            // Convert back to DTO
            var savedQuizDto = mapper.Map<GetQuizResponse>(savedQuiz);

            // Return created http code and show saved user
            return CreatedAtAction(nameof(GetQuiz), new { id = savedQuiz.Id }, savedQuizDto);
        }

    }
}
