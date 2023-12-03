
using TP3.Models;

namespace TP4.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);
        Task CreateMovie(Movie movie);
        Task EditMovie(Movie movie);
        Task DeleteMovie(int id);
        Task<List<Movie>> GetMoviesByGenre(int genreId);
        Task<List<Movie>> GetAllMoviesOrderedAscending();
        Task<List<Movie>> GetMoviesByUserDefinedGenre(string userDefinedGenre);
    }
}
