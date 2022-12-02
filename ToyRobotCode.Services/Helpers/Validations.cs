using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotCode.Services.Enums;
using ToyRobotCode.Services.Models;

namespace ToyRobotCode.Services.Helpers
{
    public static class Validations
    {
        private const string RobotWillFallMsg = "Robot will fall to the table.";

        /// <summary>
        /// This method will validate the following checks below.
        /// <list type="bullet">
        /// <item>If Robot is already placed on the table before calling MOVE, LEFT OR RIGHT, and REPORT.</item>
        /// <item>If the Robot is initially placed on the table within the configured setup(which is 5x5) </item>
        /// </list>
        /// </summary>
        /// <returns>Boolean whether true or false.</returns>
        public static bool RobotValidations(List<string> errors, Robot robot, TableDimensions table)
        {
            errors.Clear();

            if (robot.Position == null) errors.Add("Robot should be placed in the table before any commands.");
            else if (robot.Position.X > table.Length || robot.Position.Y > table.Width) errors.Add("Robot placement is exceeds the table dimension.");
            return !errors.Any();
        }

        /// <summary>
        /// This method will check if the next movement of the Robot will exceed and fall off the table.
        /// </summary>
        /// <returns>Boolean whether true or false.</returns>
        public static bool RobotValidations(List<string> errors, Robot robot, TableDimensions table, Directions direction = default)
        {
            errors.Clear();

            if (robot.Position == null) errors.Add("Robot should be placed in the table before any commands.");
            else if (robot.Position.X > table.Length || robot.Position.Y > table.Width) errors.Add("Robot placement is exceeds the table dimension.");

            var currentX = robot.Position.X;
            var currentY = robot.Position.Y;

            switch (direction)
            {
                case Directions.North:
                    currentY++;
                    if (currentY > table?.Width) errors.Add(RobotWillFallMsg);
                    break;
                case Directions.South:
                    currentY--;
                    if (currentY < 0) errors.Add(RobotWillFallMsg);
                    break;
                case Directions.East:
                    currentX++;
                    if (currentX > table?.Length) errors.Add(RobotWillFallMsg);
                    break;
                case Directions.West:
                    currentX--;
                    if (currentX < 0) errors.Add(RobotWillFallMsg);
                    break;
            }

            return !errors.Any();
        }
    }
}
