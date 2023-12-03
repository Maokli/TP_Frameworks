// MovieRepository.cs
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TP3.Models;

namespace TP4.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task CreateMovie(Movie movie)
        {
            if (movie.ImageFile != null && movie.ImageFile.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", movie.ImageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    movie.ImageFile.CopyTo(stream);
                }

                movie.Photo = $"/images/{movie.ImageFile.FileName}";
            }

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task EditMovie(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId)
        {
            return await _context.Movies
                .Where(m => m.GenresId == genreId)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetAllMoviesOrderedAscending()
        {
            return await _context.Movies
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesByUserDefinedGenre(string userDefinedGenre)
        {
            return await _context.Movies
                .Where(m => m.Genres.GenreName == userDefinedGenre)
                .ToListAsync();
        }
    }
}
