# Toy Robot Code Challenge by Raymarc Ramos
Project Type: Console Application<br>
Framework: (Legacy) .NET Framework 4.8

# Description
The application is a simulation of a toy robot moving on a square table top, of dimensions 5 units x 5 units. There are no
other obstructions on the table surface. The robot is free to roam around the surface of the table, but must be prevented
from falling to destruction. Any movement that would result in the robot falling from the table must be prevented,
however further valid movement commands must still be allowed.

# Commands and Constraints
- ``PLACE`` - This will place and set the initial position of the robot. 
  - ``PLACE X, Y, Direction: North,East,South,West,`` e.g. ``Place 0,0,North``.
- ``MOVE`` - This will move the toy robot one unit forward in the direction it is currently facing.
- ``LEFT`` - This will rotate the robot 90 degrees to the left without changing the position of the robot.
- ``RIGHT`` - This will rotate the robot 90 degrees to the right without changing the position of the robot.
- ``REPORT`` - This will return the X,Y, and Direction of the robot. e.g. ``REPORT`` will return ``0,1, East``
- ``EXIT`` - This will exit the application.

The toy robot must not fall off the table during movement. This also includes the initial placement of the toy robot. Any
move that would cause the robot to fall must be ignored.

# How to run the Application
- Clone or Download the repository.
- Open with Visual Studio.
- Open ToyRobotCode.sln solution.
- Start the Console Application Project ``ToyRobotCode``.
- Unit tests are all passed and located in ``ToyRobotCode.Test``.
- ``ToyRobotCode.Services`` is a class library that contains all the building blocks to run and process the toy robot.
