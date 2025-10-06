using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using MVCClient.Controllers;
using MVCClient.Models;
using System.Collections.Generic;
using Xunit;

namespace MVCClientTests
{
    public class MovieControllerTests
    {
        public class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Movie> Movies { get; } = new Movie[] {
                new Movie { Name = "Gone with the Wind", Star = "Clark Gable" ,   YearReleased = 1900, Rating = 1M},
                new Movie { Name = "Grease",             Star = "John Travolta" , YearReleased = 2005, Rating = 3M},
                new Movie { Name = "Jaws",               Star = "Roy Scheider" ,  YearReleased = 1996, Rating = 2M},
                new Movie { Name = "Harry Potter and the Goblet of Fire", Star = "Daniel Radcliffe" , YearReleased = 2021, Rating = 5M}
            };

            public void AddMovie(Movie m)
            {
                // do nothing - not required for test
            }
        }

        public class HomeControllerTests
        {
            [Fact]
            public void IndexActionModelNameAndStar()
            {
                // Arrange
                var controller        = new MoviesController(new NullLogger<MoviesController>());
                controller.Repository = new ModelCompleteFakeRepository();
                // Act
                var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Movie>;
                // Assert
                Assert.Equal(controller.Repository.Movies, model, Comparer.Get<Movie>((p1, p2) => p1.Name == p2.Name && p1.Star == p2.Star));
            }

            [Fact]
            public void IndexActionModelYearReleased()
            {
                // Arrange
                var controller = new MoviesController(new NullLogger<MoviesController>());
                controller.Repository = new ModelCompleteFakeRepository();
                // Act
                var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Movie>;
                // Assert
                Assert.NotEqual(controller.Repository.Movies, model, Comparer.Get<Movie>((p1, p2) => p1.YearReleased == p2.YearReleased));
            }
        }
    }
}
