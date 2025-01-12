
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesWeb.Controllers;

public class GamesController : Controller
{
    private readonly IDAO _dao;

    public GamesController(BLC.BLC blc)
    {
        _dao = blc.Dao;
    }

    // GET: Games
    public async Task<IActionResult> Index()
    {
        var games = _dao.GetAllGames();
        return View(games);
    }

    // GET: Games/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var game = _dao.GetAllGames().FirstOrDefault(x => x.Id == id);
        if (game == null) return NotFound();

        return View(game);
    }

    // GET: Games/Create
    public IActionResult Create()
    {
        ViewData["Producer"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
        return View();
    }

    // POST: Games/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int id, int producerId, string name, int releaseYear, int genre, double price, double diskSpace, int rating)
    {
        var game = _dao.CreateNewGame();
        game.Id = id;
        game.Name = name;
        game.ReleaseYear = releaseYear;
        game.Genre = (GameGenre)genre;
        game.Price = price;
        game.DiskSpace = diskSpace;
        game.Rating = rating;
        game.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);

        ModelState.Clear();
        TryValidateModel(game);
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        if (ModelState.IsValid)
        {
            _dao.AddGame(game);
            _dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewData["Producer"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
        return View(game);
    }

    // GET: Games/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var game = _dao.GetAllGames().FirstOrDefault(x => x.Id == id);

        if (game == null) return NotFound();

        ViewData["Producer"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
        return View(game);
    }

    // POST: Games/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, int producerId, string name, int releaseYear, int genre, double price, double diskSpace, int rating)

    {
        IGame game = _dao.GetAllGames().FirstOrDefault(c => c.Id == id);
        IGame gameTemp = _dao.CreateNewGame();

        if (id != game.Id)
        {
            return NotFound();
        }
        
        gameTemp.Id = id;
        gameTemp.Name = name;
        gameTemp.ReleaseYear = releaseYear;
        gameTemp.Genre = (GameGenre)genre;
        gameTemp.Price = price;
        gameTemp.DiskSpace = diskSpace;
        gameTemp.Rating = rating;
        gameTemp.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);

        ModelState.Clear();
        this.TryValidateModel(gameTemp);

        if (ModelState.IsValid)
        {
            game.Id = gameTemp.Id;
            game.Name = gameTemp.Name;
            game.ReleaseYear = gameTemp.ReleaseYear;
            game.Genre = gameTemp.Genre;
            game.Price = gameTemp.Price;
            game.DiskSpace = gameTemp.DiskSpace;
            game.Rating = gameTemp.Rating;
            game.Producer = gameTemp.Producer;

            try
            {
                _dao.SaveChanges();
            }
            catch (Exception e)
            {
                if (!GameExists(game.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["Producer"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
        return View(gameTemp);
    }

    // GET: Games/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var game = _dao.GetAllGames().FirstOrDefault(x => x.Id == id);
        if (game == null) return NotFound();
        return View(game);
    }

    // POST: Games/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var game = _dao.GetAllGames().FirstOrDefault(x => x.Id == id);
        if (game != null)
        {
            _dao.RemoveGame(game);
        }
        _dao.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool GameExists(int id)
    {
        return _dao.GetAllGames().Any(e => e.Id == id);
    }
}