using Microsoft.Extensions.DependencyInjection;
using WebAsp.Data;
using WebAsp.Models;
using WebAsp.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace WebAsp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                try
                {
                    // Increase command timeout for the seeding process
                    context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300)); // 5 minutes

                    // Ensure database is created
                    context.Database.EnsureCreated();

                    // Seed data in a single transaction to improve performance
                    if (!context.Cinemas.Any() || !context.Actors.Any() || !context.Producers.Any() ||
                        !context.Movies.Any() || !context.Actor_Movies.Any())
                    {
                        var cinemas = new List<Cinema>
                        {
                            new Cinema { Name = "Cinema 1", Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg", Description = "This is the description of the first cinema" },
                            new Cinema { Name = "Cinema 2", Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg", Description = "This is the description of the second cinema" },
                            new Cinema { Name = "Cinema 3", Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg", Description = "This is the description of the third cinema" },
                            new Cinema { Name = "Cinema 4", Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg", Description = "This is the description of the fourth cinema" },
                            new Cinema { Name = "Cinema 5", Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg", Description = "This is the description of the fifth cinema" }
                        };

                        var actors = new List<Actor>
                        {
                            new Actor { FullName = "Actor 1", Bio = "This is the Bio of the first actor", ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg" },
                            new Actor { FullName = "Actor 2", Bio = "This is the Bio of the second actor", ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg" },
                            new Actor { FullName = "Actor 3", Bio = "This is the Bio of the third actor", ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg" },
                            new Actor { FullName = "Actor 4", Bio = "This is the Bio of the fourth actor", ProfilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg" },
                            new Actor { FullName = "Actor 5", Bio = "This is the Bio of the fifth actor", ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg" }
                        };

                        var producers = new List<Producer>
                        {
                            new Producer { FullName = "Producer 1", Bio = "This is the Bio of the first producer", ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg" },
                            new Producer { FullName = "Producer 2", Bio = "This is the Bio of the second producer", ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg" },
                            new Producer { FullName = "Producer 3", Bio = "This is the Bio of the third producer", ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg" },
                            new Producer { FullName = "Producer 4", Bio = "This is the Bio of the fourth producer", ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg" },
                            new Producer { FullName = "Producer 5", Bio = "This is the Bio of the fifth producer", ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg" }
                        };

                        var movies = new List<Movie>
                        {
                            new Movie { Name = "Life", Description = "This is the Life movie description", Price = 39.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10), CinemaId = 3, ProducerId = 3, MovieCategory = MovieCategory.Documentary },
                            new Movie { Name = "The Shawshank Redemption", Description = "This is the Shawshank Redemption description", Price = 29.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(3), CinemaId = 1, ProducerId = 1, MovieCategory = MovieCategory.Action },
                            new Movie { Name = "Ghost", Description = "This is the Ghost movie description", Price = 39.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), CinemaId = 4, ProducerId = 4, MovieCategory = MovieCategory.Horror },
                            new Movie { Name = "Race", Description = "This is the Race movie description", Price = 39.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5), CinemaId = 1, ProducerId = 2, MovieCategory = MovieCategory.Documentary },
                            new Movie { Name = "Scoob", Description = "This is the Scoob movie description", Price = 39.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-2), CinemaId = 1, ProducerId = 3, MovieCategory = MovieCategory.Cartoon },
                            new Movie { Name = "Cold Soles", Description = "This is the Cold Soles movie description", Price = 39.50.ToString("0.00"), ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg", StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(20), CinemaId = 1, ProducerId = 5, MovieCategory = MovieCategory.Drama }
                        };

                        var actorMovies = new List<Actor_Movie>
                        {
                            new Actor_Movie { ActorId = 1, MovieId = 1 },
                            new Actor_Movie { ActorId = 3, MovieId = 1 },
                            new Actor_Movie { ActorId = 1, MovieId = 2 },
                            new Actor_Movie { ActorId = 4, MovieId = 2 },
                            new Actor_Movie { ActorId = 1, MovieId = 3 },
                            new Actor_Movie { ActorId = 2, MovieId = 3 },
                            new Actor_Movie { ActorId = 5, MovieId = 3 },
                            new Actor_Movie { ActorId = 2, MovieId = 4 },
                            new Actor_Movie { ActorId = 3, MovieId = 4 },
                            new Actor_Movie { ActorId = 4, MovieId = 4 },
                            new Actor_Movie { ActorId = 2, MovieId = 5 },
                            new Actor_Movie { ActorId = 3, MovieId = 5 },
                            new Actor_Movie { ActorId = 4, MovieId = 5 },
                            new Actor_Movie { ActorId = 5, MovieId = 5 },
                            new Actor_Movie { ActorId = 3, MovieId = 6 },
                            new Actor_Movie { ActorId = 4, MovieId = 6 },
                            new Actor_Movie { ActorId = 5, MovieId = 6 }
                        };

                        context.Cinemas.AddRange(cinemas);
                        context.Actors.AddRange(actors);
                        context.Producers.AddRange(producers);
                        context.Movies.AddRange(movies);
                        context.Actor_Movies.AddRange(actorMovies);
                        context.SaveChanges(); // Single SaveChanges for all data
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database seeding error: {ex.Message}");
                    throw;
                }
            }
        }
    }
}