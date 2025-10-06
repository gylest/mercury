using MVCClient.Models;
using System.Collections.Generic;

namespace MVCClient.Repositories
{
    public class MovieRepository: IRepository
    {
        private static MovieRepository sharedRepository = new MovieRepository();

        readonly Dictionary<string, Movie> movies = new Dictionary<string, Movie>();

        public static MovieRepository SharedRepository => sharedRepository;

        public MovieRepository()
        {
            var initialItems = new[] {
                                       new Movie { Name = "Gone with the Wind", Star = "Clark Gable" ,   YearReleased = 1939, Rating = 7.6M},
                                       new Movie { Name = "Grease",             Star = "John Travolta" , YearReleased = 1978, Rating = 8.5M},
                                       new Movie { Name = "Jaws",               Star = "Roy Scheider" ,  YearReleased = 1975, Rating = 9.1M},
                                       new Movie { Name = "Harry Potter and the Goblet of Fire", Star = "Daniel Radcliffe" , YearReleased = 2005, Rating = 9.1M}
            };

            foreach (var p in initialItems)
            {
                AddMovie(p);
            }
        }

        public IEnumerable<Movie> Movies => movies.Values;

        public void AddMovie(Movie m)
        {
            if (m==null)
            {
                throw new System.ArgumentNullException(nameof(m));
            }

            movies.Add(m.Name, m);

        }

    }

}

