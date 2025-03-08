using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podsosus2.Data;
using Podsosus2.Models;

namespace Podsosus2.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Базовый маршрут: /api/Home
    public class HomeController : ControllerBase // Используем ControllerBase для API
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Словарь для хранения данных о клетках
        private readonly Dictionary<string, Cell> _cells = new Dictionary<string, Cell>
        {
            { "Start", new Cell { Title = "Start" } },
            { "Room1", new Cell { Title = "Room1" } },
            { "Room2", new Cell { Title = "Room2" } },
            { "BossRoom", new Cell { Title = "BossRoom" } }
        };

        // GET: api/Home/getplayers
        [HttpGet("getplayers")]
        public ActionResult GetPlayers()
        {
            if (_context.Players == null)
            {
                return NotFound(new { message = "Players data not available." });
            }
            var players = _context.Players.ToList();
            return Ok(new { players = players });
        }

        // GET: api/Home/getcoordinates?name=someName
        [HttpGet("getcoordinates")]
        public ActionResult<Cell> GetCoordinates(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Name parameter is required." });
            }

            if (_cells.TryGetValue(name, out var cell))
            {
                return Ok(cell);
            }

            return NotFound(new { message = $"Клетка с названием \"{name}\" не найдена." });
        }

        // Тестовый маршрут для корневого пути
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Hello from Podsosus2!";
        }
    }
}