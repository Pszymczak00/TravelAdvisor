using TravelAdvisor.Backend.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Image = TravelAdvisor.Backend.Models.Image;

namespace TravelAdvisor.Backend.Data
{
    public static class DataGenerator
    {
        private static readonly Random Random = new Random();

        private static TimeSpan GenerateExpectedVisitTime(string[] tags)
        {
            // Bazowy czas zwiedzania
            double baseHours = 0.5; // Startujemy od 30 minut

            // Dodatkowy czas w zależności od typu miejsca
            if (tags.Contains("Muzeum"))
            {
                baseHours += Random.Next(2, 7) * 0.5; // 1-3.5 godziny dodatkowo
            }
            else if (tags.Contains("Park"))
            {
                baseHours += Random.Next(1, 5) * 0.5; // 0.5-2.5 godziny dodatkowo
            }
            else if (tags.Contains("Restauracja"))
            {
                baseHours += Random.Next(1, 4) * 0.5; // 0.5-2 godziny dodatkowo
            }
            else if (tags.Contains("Kultura"))
            {
                baseHours += Random.Next(2, 6) * 0.5; // 1-3 godziny dodatkowo
            }

            // Konwertuj godziny na TimeSpan
            int hours = (int)baseHours;
            int minutes = (int)((baseHours - hours) * 60);
            return new TimeSpan(hours, minutes, 0);
        }

        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Places.Any() || context.Tags.Any())
            {
                return;
            }

            // Generowanie tagów
            var tags = new[]
            {
                new Tag { Id = Guid.NewGuid(), Name = "Restauracja" },
                new Tag { Id = Guid.NewGuid(), Name = "Muzeum" },
                new Tag { Id = Guid.NewGuid(), Name = "Park" },
                new Tag { Id = Guid.NewGuid(), Name = "Kultura" },
                new Tag { Id = Guid.NewGuid(), Name = "Rozrywka" }
            };

            context.Tags.AddRange(tags);

            var places = new[]
            {
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Pałac Kultury i Nauki",
                    Street = "Plac Defilad",
                    StreetNumber = "1",
                    PostalCode = "00-901",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.231838,
                    Longitude = 21.005995,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateStandardOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Łazienki Królewskie",
                    Street = "Agrykola",
                    StreetNumber = "1",
                    PostalCode = "00-460",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.215117,
                    Longitude = 21.035725,
                    Tags = new[] { tags[2], tags[3] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum Powstania Warszawskiego",
                    Street = "Grzybowska",
                    StreetNumber = "79",
                    PostalCode = "00-844",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.232222,
                    Longitude = 20.981111,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Różana",
                    Street = "Chocimska",
                    StreetNumber = "7",
                    PostalCode = "00-791",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.208889,
                    Longitude = 21.021944,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Park Fontann",
                    Street = "Skwer I Dywizji Pancernej",
                    StreetNumber = "1",
                    PostalCode = "00-221",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.254722,
                    Longitude = 21.012778,
                    Tags = new[] { tags[2], tags[4] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Stary Dom",
                    Street = "Puławska",
                    StreetNumber = "104/106",
                    PostalCode = "02-620",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.198333,
                    Longitude = 21.021111,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Park Skaryszewski",
                    Street = "Aleja Zieleniecka",
                    StreetNumber = "1",
                    PostalCode = "03-847",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.245556,
                    Longitude = 21.053889,
                    Tags = new[] { tags[2] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum Narodowe",
                    Street = "Aleje Jerozolimskie",
                    StreetNumber = "3",
                    PostalCode = "00-495",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.232222,
                    Longitude = 21.024444,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja U Fukiera",
                    Street = "Rynek Starego Miasta",
                    StreetNumber = "27",
                    PostalCode = "00-272",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.249722,
                    Longitude = 21.012222,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Ogród Saski",
                    Street = "Marszałkowska",
                    StreetNumber = "1",
                    PostalCode = "00-102",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.240833,
                    Longitude = 21.008889,
                    Tags = new[] { tags[2] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Teatr Wielki - Opera Narodowa",
                    Street = "Plac Teatralny",
                    StreetNumber = "1",
                    PostalCode = "00-950",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.243889,
                    Longitude = 21.009722,
                    Tags = new[] { tags[3], tags[4] }.ToList(),
                    OpeningHours = GenerateTheatreOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Kultura", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Belvedere",
                    Street = "Agrykola",
                    StreetNumber = "1",
                    PostalCode = "00-460",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.215833,
                    Longitude = 21.034444,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Zamek Królewski",
                    Street = "Plac Zamkowy",
                    StreetNumber = "4",
                    PostalCode = "00-277",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.247778,
                    Longitude = 21.014722,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Centrum Nauki Kopernik",
                    Street = "Wybrzeże Kościuszkowskie",
                    StreetNumber = "20",
                    PostalCode = "00-390",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2418,
                    Longitude = 21.0284,
                    Tags = new[] { tags[1], tags[3], tags[4] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Zoo Warszawa",
                    Street = "Ratuszowa",
                    StreetNumber = "1/3",
                    PostalCode = "03-461",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2549,
                    Longitude = 21.0234,
                    Tags = new[] { tags[2], tags[4] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Stixx",
                    Street = "Plac Europejski",
                    StreetNumber = "4A",
                    PostalCode = "00-844",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2322,
                    Longitude = 20.9845,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum POLIN",
                    Street = "Anielewicza",
                    StreetNumber = "6",
                    PostalCode = "00-157",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2497,
                    Longitude = 20.9925,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Park Ujazdowski",
                    Street = "Aleje Ujazdowskie",
                    StreetNumber = "6",
                    PostalCode = "00-461",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2225,
                    Longitude = 21.0242,
                    Tags = new[] { tags[2] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Hala Koszyki",
                    Street = "Koszykowa",
                    StreetNumber = "63",
                    PostalCode = "00-667",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2225,
                    Longitude = 21.0119,
                    Tags = new[] { tags[0], tags[4] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum Wojska Polskiego",
                    Street = "Aleje Jerozolimskie",
                    StreetNumber = "3",
                    PostalCode = "00-495",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2321,
                    Longitude = 21.0244,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Der Elefant",
                    Street = "Plac Bankowy",
                    StreetNumber = "1",
                    PostalCode = "00-139",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2429,
                    Longitude = 21.0027,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Park Agrykola",
                    Street = "Myśliwiecka",
                    StreetNumber = "9",
                    PostalCode = "00-459",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2156,
                    Longitude = 21.0317,
                    Tags = new[] { tags[2], tags[4] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Teatr Roma",
                    Street = "Nowogrodzka",
                    StreetNumber = "49",
                    PostalCode = "00-695",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2285,
                    Longitude = 21.0136,
                    Tags = new[] { tags[3], tags[4] }.ToList(),
                    OpeningHours = GenerateTheatreOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Kultura", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum Neonów",
                    Street = "Mińska",
                    StreetNumber = "25",
                    PostalCode = "03-808",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2461,
                    Longitude = 21.0644,
                    Tags = new[] { tags[1], tags[3], tags[4] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Restauracja Warszawa Wschodnia",
                    Street = "Mińska",
                    StreetNumber = "25",
                    PostalCode = "03-808",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2461,
                    Longitude = 21.0644,
                    Tags = new[] { tags[0] }.ToList(),
                    OpeningHours = GenerateRestaurantOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Restauracja" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Park Szczęśliwicki",
                    Street = "Drawska",
                    StreetNumber = "22",
                    PostalCode = "02-202",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2086,
                    Longitude = 20.9603,
                    Tags = new[] { tags[2], tags[4] }.ToList(),
                    OpeningHours = GenerateParkOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Park", "Rozrywka" })
                },
                new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "Muzeum Sztuki Nowoczesnej",
                    Street = "Pańska",
                    StreetNumber = "3",
                    PostalCode = "00-124",
                    City = "Warszawa",
                    Country = "Polska",
                    Latitude = 52.2331,
                    Longitude = 21.0019,
                    Tags = new[] { tags[1], tags[3] }.ToList(),
                    OpeningHours = GenerateMuseumOpeningHours(),
                    ExpectedVisitTime = GenerateExpectedVisitTime(new[] { "Muzeum", "Kultura" })
                }
            };

            // Tymczasowo pomijamy generowanie obrazów
            foreach (var place in places)
            {
                place.Images = new List<Image>
                {
                    new Image
                    {
                        Id = Guid.NewGuid(),
                        FileName = "placeholder.jpg",
                        ContentType = "image/jpeg",
                        Data = new byte[0], // Pusty array zamiast prawdziwego obrazu
                        PlaceId = place.Id
                    }
                };
            }

            context.Places.AddRange(places);
            context.SaveChanges();
        }

        private static List<OpeningHours> GenerateStandardOpeningHours()
        {
            var hours = new List<OpeningHours>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                hours.Add(new OpeningHours
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = day,
                    OpenTime = new TimeOnly(9, 0),
                    CloseTime = new TimeOnly(17, 0),
                    IsClosed = day == DayOfWeek.Sunday
                });
            }
            return hours;
        }

        private static List<OpeningHours> GenerateMuseumOpeningHours()
        {
            var hours = new List<OpeningHours>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                hours.Add(new OpeningHours
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = day,
                    OpenTime = new TimeOnly(10, 0),
                    CloseTime = new TimeOnly(18, 0),
                    IsClosed = day == DayOfWeek.Monday
                });
            }
            return hours;
        }

        private static List<OpeningHours> GenerateRestaurantOpeningHours()
        {
            var hours = new List<OpeningHours>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                hours.Add(new OpeningHours
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = day,
                    OpenTime = new TimeOnly(12, 0),
                    CloseTime = new TimeOnly(23, 0),
                    IsClosed = false
                });
            }
            return hours;
        }

        private static List<OpeningHours> GenerateParkOpeningHours()
        {
            var hours = new List<OpeningHours>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                hours.Add(new OpeningHours
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = day,
                    OpenTime = new TimeOnly(6, 0),
                    CloseTime = new TimeOnly(22, 0),
                    IsClosed = false
                });
            }
            return hours;
        }

        private static List<OpeningHours> GenerateTheatreOpeningHours()
        {
            var hours = new List<OpeningHours>();
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                hours.Add(new OpeningHours
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = day,
                    OpenTime = new TimeOnly(11, 0),
                    CloseTime = new TimeOnly(23, 0),
                    IsClosed = day == DayOfWeek.Monday
                });
            }
            return hours;
        }

        private static byte[] GenerateRandomImage()
        {
            int width = 800;
            int height = 600;
            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            
            // Wypełnij tło losowym kolorem
            using var brush = new SolidBrush(Color.FromArgb(
                Random.Next(256),
                Random.Next(256),
                Random.Next(256)
            ));
            graphics.FillRectangle(brush, 0, 0, width, height);

            // Dodaj losowe kształty
            for (int i = 0; i < 10; i++)
            {
                using var shapeBrush = new SolidBrush(Color.FromArgb(
                    Random.Next(128, 256),
                    Random.Next(128, 256),
                    Random.Next(128, 256)
                ));

                var x = Random.Next(width);
                var y = Random.Next(height);
                var size = Random.Next(50, 200);

                switch (Random.Next(3))
                {
                    case 0: // Koło
                        graphics.FillEllipse(shapeBrush, x, y, size, size);
                        break;
                    case 1: // Kwadrat
                        graphics.FillRectangle(shapeBrush, x, y, size, size);
                        break;
                    case 2: // Trójkąt
                        graphics.FillPolygon(shapeBrush, new Point[]
                        {
                            new Point(x, y + size),
                            new Point(x + size / 2, y),
                            new Point(x + size, y + size)
                        });
                        break;
                }
            }

            // Konwertuj do JPEG
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
} 