using NUnit.Framework;
using System;
using ToyRobotCode.Services.Enums;
using ToyRobotCode.Services.Interfaces;
using ToyRobotCode.Services.Models;
using ToyRobotCode.Services.Services;

namespace ToyRobotCode.UnitTest
{
    [TestFixture]
    public sealed class ToyRobotTests
    {
        private readonly ITable _table;
        private readonly ToyRobotService _robotService;
        private TableDimensions _tableDimensions;
        private const int length = 5;
        private const int width = 5;

        public ToyRobotTests()
        {
            _robotService = new ToyRobotService();
            _table = new TableService();
            _tableDimensions = _table.SetupTable(length, width);
        }

        [Test]
        public void ShouldSuccessWhenRobotIsPlacedOnTheTable()
        {
            var position = new Position(0, 0, Directions.North);

            _robotService.PlaceRobot(position);
            var response = _robotService.MoveRobot();

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Ok));
            Assert.That(_robotService.Robot.Position, Is.Not.Null);
        }

        [Test]
        public void ShouldSuccessWhenRobotIsMoved()
        {
            var actualPosition = new Position(0, 0, Directions.North);
            var expectedPosition = new Position(0, 1, Directions.North);

            _robotService.PlaceRobot(actualPosition);
            var response = _robotService.MoveRobot();

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Ok));
            Assert.That(actualPosition, Is.Not.EqualTo(expectedPosition));
        }

        [Test]
        public void ShouldFailWhenRobotIsMovedWithoutPlacingOnTheTable()
        {
            _robotService.Robot.Position = null;

            var response = _robotService.MoveRobot();

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Invalid));
            Assert.That(response.Errors, Has.One.EqualTo("Robot should be placed in the table before any commands."));
        }

        [Test]
        public void ShouldFailWhenRobotIsPlacedOutsideTheTableDimensions()
        {
            var actualPosition = new Position(5, 6, Directions.North);

            var response = _robotService.PlaceRobot(actualPosition);

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Invalid));
            Assert.That(response.Errors, Has.One.EqualTo("Robot placement is exceeds the table dimension."));
        }

        [Test]
        [TestCase(5, 5, Directions.North)]
        [TestCase(0, 0, Directions.West)]
        [TestCase(0, 0, Directions.South)]
        [TestCase(5, 5, Directions.East)]
        public void ShouldFailWhenRobotIsMovedBeyondTheTableDimensions(int x, int y, Directions direction)
        {
            var actualPosition = new Position(x, y, direction);

            _robotService.PlaceRobot(actualPosition);
            var response = _robotService.MoveRobot();

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Invalid));
            Assert.That(response.Errors, Has.One.EqualTo("Robot will fall to the table."));
        }

        [Test]
        [TestCase(Directions.North)]
        [TestCase(Directions.West)]
        [TestCase(Directions.South)]
        [TestCase(Directions.East)]
        public void ShouldSuccessWhenRobotIsTurnedToLeft(Directions direction)
        {
            _robotService.PlaceRobot(new Position(0, 0, direction)); 

            var response = _robotService.TurnLeft();

            switch (direction)
            {
                case Directions.North:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.West));
                    break;
                case Directions.West:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.South));
                    break;
                case Directions.South:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.East));
                    break;
                case Directions.East:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.North));
                    return;
                default:
                    break;
            }
            Assert.That(response.Code, Is.EqualTo(ResponseCode.Ok));
            Assert.That(response.Message, Is.EqualTo("Robot turned left."));
        }

        [Test]
        [TestCase(Directions.North)]
        [TestCase(Directions.West)]
        [TestCase(Directions.South)]
        [TestCase(Directions.East)]
        public void ShouldSuccessWhenRobotIsTurnedToRight(Directions direction)
        {
            _robotService.PlaceRobot(new Position(0, 0, direction));

            var response = _robotService.TurnRight();

            switch (direction)
            {
                case Directions.North:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.East));
                    break;
                case Directions.East:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.South));
                    break;
                case Directions.South:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.West));
                    return;
                case Directions.West:
                    Assert.That(_robotService.Robot.Position.Directions, Is.EqualTo(Directions.North));
                    break;
                default:
                    break;
            }
            Assert.That(response.Code, Is.EqualTo(ResponseCode.Ok));
            Assert.That(response.Message, Is.EqualTo("Robot turned right."));
        }

        [Test]
        public void ShouldSuccessWhenReportIsCalled()
        {
            _robotService.PlaceRobot(new Position(2, 3, Directions.East));

            var response = _robotService.Report();

            Assert.That(response.Code, Is.EqualTo(ResponseCode.Ok));
            Assert.That(response.Message, Is.EqualTo("2,3,East"));
        }
    }
}
