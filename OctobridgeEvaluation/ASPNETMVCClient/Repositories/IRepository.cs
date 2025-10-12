namespace MVCClient.Models;

public interface IRepository
{
    IEnumerable<Movie> Movies { get; }
    void AddMovie(Movie m);
}
