using MVCClient.Models;
using Xunit;

namespace MVCClientTests
{
    public class MovieTests
    {
        [Fact]
        public void CanChangeMovieName()
        {
            // Arrange
            var m = new Movie { Name = "Mission Impossible 4", Star = "Tom Cruise", YearReleased = 2011, Rating = 7.7M };
            // Act
            m.Name = "Blubber Street";
            //Assert
            Assert.Equal("Blubber Street", m.Name);
        }

        [Fact]
        public void CanChangeMovieRating()
        {
            // Arrange
            var m = new Movie { Name = "Star Trek The Motion Picture", Star = "William Shatner", YearReleased = 1980, Rating = 8.6M };
            // Act
            m.Rating = 10.0M;
            //Assert
            Assert.Equal(10.0M, m.Rating);
        }
    }
}
