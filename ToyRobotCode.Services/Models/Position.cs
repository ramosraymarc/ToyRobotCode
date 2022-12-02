using ToyRobotCode.Services.Enums;

namespace ToyRobotCode.Services.Models
{
    public sealed class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Directions { get; set; }
        public Position(int x, int y, Directions direction)
        {
            X = x;
            Y = y;
            Directions = direction;
        }
    }
}
