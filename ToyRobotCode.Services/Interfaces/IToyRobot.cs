using ToyRobotCode.Services.Enums;
using ToyRobotCode.Services.Models;

namespace ToyRobotCode.Services.Interfaces
{
    public interface IToyRobot
    {
        Response PlaceRobot(Position position);
        Response MoveRobot();
        Response TurnLeft();
        Response TurnRight();
        Response Report();
        Response Exit();
    }
}
