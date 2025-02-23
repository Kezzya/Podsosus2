using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Podsosus2.Data;
using Podsosus2.Models;

namespace Podsosus2.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Это задает базовый маршрут "api/Home"
    public class HomeController : Controller
    {
        readonly ApplicationDbContext context;
        // Словарь для хранения данных о клетках
        private readonly Dictionary<string, Cell> _cells = new Dictionary<string, Cell>
        {
            { "Start", new Cell { Title = "Start", X = 0, Y = 0, Z=0 } },
            { "Room1", new Cell { Title = "Room1", X = 1, Y = 0, Z=0 } },
            { "Room2", new Cell { Title = "Room2", X = 2, Y = 0, Z=0 } },
            { "Room3", new Cell { Title = "BossRoom", X = 3, Y = 0 , Z = 0} },
            // Добавьте остальные клетки по аналогии
        };
        [Route("api/getplayers/")]
        [HttpGet()]
        public ActionResult GetPlayers()
        {
            var players = context.Players;
            return new JsonResult(new { players = players });

        }
        // GET api/home/{name}
        [Route("api/getcoordinates/")]
        [HttpGet()]
        public ActionResult<Cell> GetCoordinates(string name)
        {
            if (_cells.TryGetValue(name, out var cell))
            {
                return Ok(cell); // Возвращаем координаты клетки
            }

            return NotFound(new { message = $"Клетка с названием \"{name}\" не найдена." });
        }

        // GET: Cell
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cell/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cell/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cell/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cell/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cell/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cell/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cell/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }


}
