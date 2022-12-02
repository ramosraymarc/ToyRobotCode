using System;
using System.Linq;
using ToyRobotCode.Services.Enums;
using ToyRobotCode.Services.Models;
using ToyRobotCode.Services.Services;

namespace ToyRobotCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Toy Robot App");
            Console.WriteLine("Commands: PLACE X, Y, DIRECTION/MOVE/LEFT OR RIGHT/REPORT");
            Console.WriteLine("Example");
            Console.WriteLine(">PLACE 0,0, NORTH");
            Console.WriteLine(">MOVE");
            Console.WriteLine(">LEFT");
            Console.WriteLine(">REPORT");
            Console.WriteLine(">Output: 0,1,NORTH");
            Console.WriteLine(">Type Exit to close the application.");
            Console.WriteLine("*-----------------------------------*");
            Console.WriteLine(">Start input:");

            Console.WriteLine(Environment.NewLine);

            var robotService = new ToyRobotService();

            var isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("Please key in a command.");
                var command = Console.ReadLine();

                switch (command?.ToLowerInvariant())
                {
                    case var input 
                    when command.Contains(nameof(Commands.place)):
                        var commands = input.Length > 13
                            ? input.Replace("place", string.Empty).Split(',') : null;

                        if (commands == null) goto default;
                        var x = Convert.ToInt32(commands[0]);
                        var y = Convert.ToInt32(commands[1]);
                        var direction = (Directions)Enum.Parse(typeof(Directions), commands[2], true);

                        robotService.PlaceRobot(new Position(x, y, direction));
                        break;
                    case nameof(Commands.move):
                        robotService.MoveRobot();
                        break;
                    case nameof(Commands.left):
                        robotService.TurnLeft();
                        break;
                    case nameof(Commands.right):
                        robotService.TurnRight();
                        break;
                    case nameof(Commands.report):
                        robotService.Report();
                        break;
                    case nameof(Commands.exit):
                        isRunning = false;
                        robotService.Exit();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}
