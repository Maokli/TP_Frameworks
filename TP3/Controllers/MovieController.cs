using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using TP3.Models;
using TP4.Repositories;

namespace TP3.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovieRepository _movieRepository;

        public MovieController(AppDbContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _movieRepository.GetAllMovies());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie(Movie movie)
        {
            await _movieRepository.CreateMovie(movie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.ImageFile != null && movie.ImageFile.Length > 0)
                {
                    // Enregistrez le fichier image sur le serveur
                    var imagePath = Path.Combine("wwwroot/images", movie.ImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        movie.ImageFile.CopyTo(stream);
                    }

                    // Enregistrez le chemin de l'image dans la base de données
                    movie.Photo = $"/images/{movie.ImageFile.FileName}";
                }
                await _movieRepository.EditMovie(movie);
                return RedirectToAction("Index");

                
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            // Delete the image file from the /images folder
            if (!string.IsNullOrEmpty(movie.Photo))
            {
                var imagePath = Path.Combine("wwwroot", movie.Photo.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _movieRepository.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return Content("unable to find Id");
            var c = await _movieRepository.GetMovieById(id.Value);
            return View(c);
        }


        /*called new views for LINQ part tp4*/

        public IActionResult MoviesByGenre(int id)
        {
            var movies = _movieRepository.GetMoviesByGenre(id);
            return View("MoviesByGenre", movies);
        }


        public IActionResult MoviesOrderedAscending()
        {
            var movies = _movieRepository.GetAllMoviesOrderedAscending();
            return View("MoviesOrderedAscending", movies);
        }

        public IActionResult MoviesByUserDefinedGenre(string name)
        {
            var movies = _movieRepository.GetMoviesByUserDefinedGenre(name);
            return View("MoviesByUserDefinedGenre", movies);
        }
    }
}
