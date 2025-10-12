namespace MVCClient.Controllers;

public class MoviesController : Controller
{
    public IRepository Repository = MovieRepository.SharedRepository;
    private readonly ILogger _logger;

    public MoviesController(ILogger<MoviesController> logger)
    {
        _logger = logger;
    }

    // Get Movies
    public IActionResult Index()=> View(MovieRepository.SharedRepository.Movies);

    [HttpGet]
    public IActionResult Create() => View(new Movie());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Movie m)
    {
        if (ModelState.IsValid)
        {
            MovieRepository.SharedRepository.AddMovie(m);

            string movieJson = JsonSerializer.Serialize(m);
            _logger.LogInformation($"Movie (Post): {movieJson}");
            return RedirectToAction("Index");
        }
        return View(m);
    }
}

