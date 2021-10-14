using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpaceMachineMovementApp.Interface;
using SpaceMachineMovementApp.Model;
using SpaceMachineMovementApp.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceMachineMovementApp
{
    public class Program
    {
        private readonly ILogger<Program> logger;

        static void Main(string[] args)
        {
            int cornerX = 0, cornerY = 0;
            bool hasNewCommand = true;

            Plateu samplePlateu = GetPlateu(ref cornerX, ref cornerY);

            while (hasNewCommand)
            {
                int elementX = 0, elementY = 0;
                Direction defaultDirection = Direction.N;

                Movement movementSample = GetMovement(ref elementX, ref elementY, ref defaultDirection, samplePlateu);
                Console.WriteLine("Enter commands for robot in the plateau with just L, R, M characters ");
                var commands = Console.ReadLine().Replace(" ", "").ToUpperInvariant()?.ToCharArray()?.ToList();

                var host = CreateHostBuilder(args).Build();
                string lastCoordinates = host.Services.GetRequiredService<Program>().Run(movementSample, commands);
                if (movementSample.arriveBorder)
                    Console.WriteLine("Some commands haven't applied due to arriving border");
                Console.WriteLine($"Last coordinate is {lastCoordinates}");
                Console.WriteLine("Do you want enter new start coordinate and commands Y/N");
                hasNewCommand = Console.ReadLine().ToUpperInvariant() == "Y" ? true : false;

            }
        }

        private static Movement GetMovement(ref int elementX, ref int elementY, ref Direction defaultDirection, Plateu plateuSample)
        {
            while (elementX == 0 || elementY == 0)
            {
                Console.WriteLine("Enter start coordinates of robot in the plateau numbers and direction like 3 4 N");
                var sc = Console.ReadLine().ToUpperInvariant().Split(' ')?.ToList();
                if (sc.Count == 3)
                {
                    int.TryParse(sc.ElementAt(0), out elementX);
                    int.TryParse(sc.ElementAt(1), out elementY);
                    Enum.TryParse(sc.ElementAt(2), out defaultDirection);
                }
            }

            Movement movementSample = new(elementX, elementY, defaultDirection, plateuSample);
            return movementSample;
        }

        private static Plateu GetPlateu(ref int cornerX, ref int cornerY)
        {


            while (cornerX == 0 || cornerY == 0)
            {
                Console.WriteLine("Enter the upper-right coordinates of the square plateau with numbers has space between them like 6 6");
                var rc = Console.ReadLine().Split(' ')?.ToList();
                if (rc.Count == 2)
                {
                    int.TryParse(rc.ElementAt(0), out cornerX);
                    int.TryParse(rc.ElementAt(1), out cornerY);
                }
            }
            Plateu plateuSample = new(cornerX, cornerY);
            return plateuSample;
        }

        Program(ILogger<Program> logger)
        {
            this.logger = logger;
        }

        string Run(Movement movement, List<char> commands)
        {
            logger.LogInformation("Moving via commands.");
            string lastCoordinates = movement.ApplyCommand(commands);
            logger.LogInformation("Target has arrived.");
            return lastCoordinates;
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<Movement>();
                    services.AddScoped<IPlateu, Plateu>();
                });
        }
    }

}


