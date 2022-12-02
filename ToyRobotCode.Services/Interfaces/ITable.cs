using ToyRobotCode.Services.Models;

namespace ToyRobotCode.Services.Interfaces
{
    public interface ITable
    {
        TableDimensions SetupTable(int length, int width);
    }
}
