using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models.DTO;

namespace QuizWebAPI.Controllers
{
    /// <summary>
    /// This controller manage the game
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    [Authorize]
    public class GameController : Controller
    {
        [HttpPost]
        [Route("answer")]
        public IActionResult SaveAnswer([FromBody] UserAnswerDTO userAnswer)
        {
            return Ok(userAnswer);
        }
    }
}
