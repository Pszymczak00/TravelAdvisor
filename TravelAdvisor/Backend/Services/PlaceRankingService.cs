using Microsoft.EntityFrameworkCore;
using TravelAdvisor.Backend.Data;
using TravelAdvisor.Backend.Models;
using System;

namespace TravelAdvisor.Backend.Services
{
    public class PlaceRankingService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private const double AVERAGE_SPEED_KMH = 5.0; // Średnia prędkość przemieszczania się
        private const double EARTH_RADIUS_KM = 6371.0; // Promień Ziemi w kilometrach
        private const double FIXED_TRANSPORT_TIME_MINUTES = 10.0; // Stały czas na transport
        private const int ROUND_TO_MINUTES = 5; // Zaokrąglanie do wielokrotności 5 minut

        public PlaceRankingService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public class RankedPlace
        {
            public Place Place { get; set; }
            public double Score { get; set; }
        }

        /// <summary>
        /// Oblicza przybliżony czas podróży między dwoma miejscami przy założonej prędkości 5 km/h
        /// plus stały czas transportu 10 minut. Wynik jest zaokrąglany do pełnych 5 minut.
        /// </summary>
        /// <param name="from">Miejsce początkowe</param>
        /// <param name="to">Miejsce docelowe</param>
        /// <returns>Czas podróży w minutach</returns>
        public TimeSpan CalculateTravelTime(Place from, Place to)
        {
            // Konwersja stopni na radiany
            double lat1 = ToRadians(from.Latitude);
            double lon1 = ToRadians(from.Longitude);
            double lat2 = ToRadians(to.Latitude);
            double lon2 = ToRadians(to.Longitude);

            // Różnice współrzędnych
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            // Wzór haversine
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                      Math.Cos(lat1) * Math.Cos(lat2) *
                      Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            
            // Obliczenie odległości w kilometrach
            double distanceKm = EARTH_RADIUS_KM * c;
            
            // Obliczenie czasu podróży w minutach
            double timeMinutes = (distanceKm / AVERAGE_SPEED_KMH * 60.0) + FIXED_TRANSPORT_TIME_MINUTES;
            
            // Zaokrąglenie do pełnych 5 minut
            int roundedMinutes = (int)Math.Ceiling(timeMinutes / ROUND_TO_MINUTES) * ROUND_TO_MINUTES;
            
            // Konwersja na TimeSpan
            return TimeSpan.FromMinutes(roundedMinutes);
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        private async Task<Dictionary<string, double>> CalculateTagScores(ApplicationDbContext context)
        {
            var tagScores = new Dictionary<string, double>();
            var allOpinions = await context.UserPlaceOpinions.Include(x => x.Place.Tags).ToListAsync();

            foreach (var opinion in allOpinions)
            {
                foreach (var tag in opinion.Place.Tags)
                {
                    if (!tagScores.ContainsKey(tag.Name))
                        tagScores[tag.Name] = 0;

                    tagScores[tag.Name] += opinion.PositiveOpinions * 0.2 - opinion.NegativeOpinions * 0.1;
                }
            }

            return tagScores;
        }

        public async Task<List<RankedPlace>> GetRankedPlacesForUser(string username)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            
            // Pobierz użytkownika
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return new List<RankedPlace>();
            }

            // Oblicz globalne oceny tagów
            var tagScores = await CalculateTagScores(context);

            // Pobierz wszystkie miejsca z ich opiniami
            var places = await context.Places
                .Include(p => p.Tags)
                .Include(p => p.Images)
                .Include(p => p.OpeningHours)
                .Include(p => p.UserOpinions.Where(o => o.UserId == user.Id))
                .ToListAsync();

            var rankedPlaces = new List<RankedPlace>();

            foreach (var place in places)
            {
                // Oblicz podstawowy wynik na podstawie opinii
                double score = place.UserOpinions.Select(o => o.PositiveOpinions - o.NegativeOpinions/2.0).Sum();
                
                // Dodaj punkty za tagi
                score += place.Tags.Sum(tag => tagScores.GetValueOrDefault(tag.Name, 0));

                rankedPlaces.Add(new RankedPlace { Place = place, Score = score });
            }

            // Sortuj miejsca według wyniku (od najwyższego do najniższego)
            return rankedPlaces.OrderByDescending(rp => rp.Score).ToList();
        }
    }
} 