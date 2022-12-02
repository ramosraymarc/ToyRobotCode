using ToyRobotCode.Services.Interfaces;
using ToyRobotCode.Services.Models;

namespace ToyRobotCode.Services.Services
{
    public sealed class TableService : ITable
    {
        /// <summary>
        /// This method will initially setup the table depending on the configured length and width.
        /// </summary>
        /// <returns>Table Dimensions of length and width.</returns>
        public TableDimensions SetupTable(int length,  int width)
        {
            return new TableDimensions(length, width);
        }
    }
}
