using AutoMapper;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Modles.Domain;
using Models.Modles.DTO;

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
        private readonly IRepository quizRepository;

        public QuizController(ILogger<QuizController> logger, IMapper mapper, IRepository quizRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.quizRepository = quizRepository;
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
            var quizzes = await quizRepository.GetQuizzesAsync();
            return Ok(mapper.Map<List<QuizDTO>>(quizzes));
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
        [ProducesResponseType(typeof(QuizDTO), 201)]
        public async Task<IActionResult> GetQuiz([FromRoute] int id)
        {
            var quiz = await quizRepository.GetQuizByIdAsync(id);
            if(quiz == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<QuizDTO>(quiz));
        }

        /// <summary>
        /// Create a quiz in the system
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="201">Create a quiz in the system</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(typeof(QuizDTO), 201)]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizDTO quiz)
        {
            // Convert DTO to Domain Modell
            var newQuiz = mapper.Map<Quiz>(quiz);

            var savedQuiz = await quizRepository.CreateQuizAsync(newQuiz);
            if (savedQuiz == null)
            {
                return BadRequest();
            }

            // Convert back to DTO
            var savedQuizDto = mapper.Map<QuizDTO>(savedQuiz);

            // Return created http code and show saved user
            return CreatedAtAction(nameof(GetQuiz), new { id = savedQuiz.Id }, savedQuizDto);
        }
    }
}
