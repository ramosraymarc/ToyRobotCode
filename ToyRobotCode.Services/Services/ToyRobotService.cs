using System.Collections.Generic;
using System.Linq;
using ToyRobotCode.Services.Enums;
using ToyRobotCode.Services.Helpers;
using ToyRobotCode.Services.Interfaces;
using ToyRobotCode.Services.Models;

namespace ToyRobotCode.Services.Services
{
    public sealed class ToyRobotService : IToyRobot
    {
        private const int length = 5;
        private const int width = 5;
        private readonly List<string> errors = new List<string>();

        public Robot Robot;
        public TableDimensions Table { get; set; }

        /// <summary>
        /// Toy Robot Service Constructor Method
        /// <list type="bullet">
        /// <item>Will instantiate DTO Robot and TableService</item>
        /// <item>Will setup the table using Table Service and assign to Table object container for ToyRobotService usage</item>
        /// </list>
        /// </summary>
        public ToyRobotService()
        {
            Robot = new Robot();
            var tableService = new TableService();
            Table = tableService.SetupTable(length, width);
        }

        /// <summary>
        /// PLACE Robot will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST. 
        /// </summary>
        /// <param name="Position">Position</param>
        /// <returns>Response DTO
        /// <list type="bullet">
        /// <item>Code: Response status code.</item>
        /// <item>Errors: List of errors.</item>
        /// <item>Message: Detailed response code.</item>
        /// </list></returns>
        public Response PlaceRobot(Position position)
        {
            Robot.Position = position;

            if (!Validations.RobotValidations(errors, Robot, Table)) return Response.Invalid(errors);
    
            return Response.Success("Robot is placed.");
        }

        /// <summary>
        /// MOVE Robot will move the toy robot one unit forward in the direction it is currently facing.
        /// </summary>
        /// <returns>Response DTO
        /// <list type="bullet">
        /// <item>Code: Response status code.</item>
        /// <item>Errors: List of errors.</item>
        /// <item>Message: Detailed response code.</item>
        /// </list></returns>
        public Response MoveRobot()
        {
            if (!Validations.RobotValidations(errors, Robot, Table))
                return Response.Invalid(errors);
            if (!Validations.RobotValidations(errors, Robot, Table, Robot.Position.Directions)) 
                return Response.Invalid(errors);
            {
                switch (Robot.Position.Directions)
                {
                    case Directions.North:
                        Robot.Position.Y++;
                        break;
                    case Directions.East:
                        Robot.Position.X++;
                        break;
                    case Directions.South:
                        Robot.Position.Y--;
                        break;
                    case Directions.West:
                        Robot.Position.X--;
                        break;
                    default:
                        break;
                }
            }
            return Response.Success("Robot moved.");
        }

        /// <summary>
        /// Turn LEFT will rotate the robot 90 degrees to the left without changing the position of the robot.
        /// </summary>
        /// <returns>Response DTO
        /// <list type="bullet">
        /// <item>Code: Response status code.</item>
        /// <item>Errors: List of errors.</item>
        /// <item>Message: Detailed response code.</item>
        /// </list></returns>
        public Response TurnLeft()
        {
            if (!Validations.RobotValidations(errors, Robot, Table)) return Response.Invalid(errors);
            switch (Robot.Position.Directions)
            {
                case Directions.North:
                    Robot.Position.Directions = Directions.West;
                    break;
                case Directions.West:
                    Robot.Position.Directions = Directions.South;
                    break;
                case Directions.South:
                    Robot.Position.Directions = Directions.East;
                    break;
                case Directions.East:
                    Robot.Position.Directions = Directions.North;
                    break;
                default:
                    break;
            }
            return Response.Success("Robot turned left.");
        }

        /// <summary>
        /// Turn RIGHT will rotate the robot 90 degrees to the right without changing the position of the robot.
        /// </summary>
        /// <returns>Response DTO
        /// <list type="bullet">
        /// <item>Code: Response status code.</item>
        /// <item>Errors: List of errors.</item>
        /// <item>Message: Detailed response code.</item>
        /// </list></returns>
        public Response TurnRight()
        {
            if (!Validations.RobotValidations(errors, Robot, Table)) return Response.Invalid(errors);
            switch (Robot.Position.Directions)
            {
                case Directions.North:
                    Robot.Position.Directions = Directions.East;
                    break;
                case Directions.East:
                    Robot.Position.Directions = Directions.South;
                    break;
                case Directions.South:
                    Robot.Position.Directions = Directions.West;
                    break;
                case Directions.West:
                    Robot.Position.Directions = Directions.North;
                    break;
                default:
                    break;
            }
            return Response.Success("Robot turned right.");
        }

        /// <summary>
        /// REPORT will provide the X,Y and F details of the robot.
        /// </summary>
        /// <returns>Response DTO
        /// <list type="bullet">
        /// <item>Code: Response status code.</item>
        /// <item>Errors: List of errors.</item>
        /// <item>Message: Detailed response code.</item>
        /// </list></returns>
        public Response Report()
        {
            if (!Validations.RobotValidations(errors, Robot, Table)) return Response.Invalid(errors);
            return Response.Success($"{Robot.Position.X},{Robot.Position.Y},{Robot.Position.Directions}");
        }

        /// <summary>
        /// Will exit the application.
        /// </summary>
        public Response Exit()
        {
            return Response.Success("Exiting the app.");
        }

        //#region Validation Methods
        ///// <summary>
        ///// This method will validate the following checks below.
        ///// <list type="bullet">
        ///// <item>If Robot is already placed on the table before calling MOVE, LEFT OR RIGHT, and REPORT.</item>
        ///// <item>If the Robot is initially placed on the table within the configured setup(which is 5x5) </item>
        ///// </list>
        ///// </summary>
        ///// <returns>Boolean whether true or false.</returns>
        //private bool RobotValidations()
        //{
        //    errors.Clear();

        //    if (_robot.Position == null) errors.Add("Robot should be placed in the table before any commands.");
        //    else if (_robot.Position.X > Table.Length || _robot.Position.Y > Table.Width) errors.Add("Robot placement is exceeds the table dimension.");           
        //    return !errors.Any();
        //}

        ///// <summary>
        ///// This method will check if the next movement of the Robot will exceed and fall off the table.
        ///// </summary>
        ///// <returns>Boolean whether true or false.</returns>
        //private bool IsRobotFallingOutOfBounds(Directions direction)
        //{
        //    errors.Clear();

        //    var currentX = _robot.Position.X;
        //    var currentY = _robot.Position.Y;

        //    switch (direction)
        //    {
        //        case Directions.North:
        //            currentY++;
        //            if (currentY > Table?.Width) errors.Add(RobotWillFallMsg);
        //            break;
        //        case Directions.South:
        //            currentY--;
        //            if (currentY < 0) errors.Add(RobotWillFallMsg);
        //            break;
        //        case Directions.East:
        //            currentX++;
        //            if (currentX > Table?.Length) errors.Add(RobotWillFallMsg);
        //            break;
        //        case Directions.West:
        //            currentX--;
        //            if (currentX < 0) errors.Add(RobotWillFallMsg);
        //            break;
        //    }

        //    return !errors.Any();
        //}
        //#endregion
    }
}
