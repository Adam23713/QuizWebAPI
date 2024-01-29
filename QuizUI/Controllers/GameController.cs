using Microsoft.AspNetCore.Mvc;
using Models.Response.Quiz;
using QuizUI.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace QuizUI.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger logger;
        private readonly string WebAPIUrl = "https://localhost:7094/game";
        private readonly IHttpClientFactory httpClientFactory;

        public GameController(IHttpClientFactory httpClientFactory, ILogger<GameController> logger)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Start(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetAsync($"{WebAPIUrl}/{id}?userId={10}");
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadFromJsonAsync<GetQuizForGameResponse>();
                return View(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
            }
            return BadRequest("Quiz not start");
        }

        public async Task<IActionResult> Index()
        {
            List<QuizInfoDTO> infos = new List<QuizInfoDTO>();
            try 
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetAsync(WebAPIUrl);
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadFromJsonAsync<IEnumerable<QuizInfoDTO>>();
                if(res != null)
                {
                    infos.AddRange(res);
                }
            }
            catch (Exception ex) 
            {
                logger.LogError(ex.StackTrace);
            }
            

            return View(infos);
        }
    }
}
