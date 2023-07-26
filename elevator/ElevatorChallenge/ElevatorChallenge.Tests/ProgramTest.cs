using elevatorNS;
using buildingNS;
using floorrequestNS;
using Xunit;


namespace ElevatorChallenge.Tests;


public class ProgramTest
{
    [Fact]
    public void TestValidatorWithValidInput()
    {
        string validInput = "3";

        bool validatorResult = Program.floorRequestValidator(validInput);

        Assert.True(validatorResult);
    }

    [Fact]
    public void TestValidatorWithInvalidInput()
    {
        string inValidInput = "4D4";

        bool validatorResult = Program.floorRequestValidator(inValidInput);

        Assert.False(validatorResult);
    }

    [Fact]
    public void TestValidatorWithTooShortInput()
    {
        string inValidInput = "";

        bool validatorResult = Program.floorRequestValidator(inValidInput);

        Assert.False(validatorResult);
    }

    [Fact]
    public async Task TestListenerWithDownwardInput()
    {
        Program.downFloorRequests = new Queue<FloorRequest>();


        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(100.0, 200.0);
        Building building = new Building(numberOfBuildingFloors, elevator);
        elevator.currentFloor = elevator.floors[8];


        string floorRequestString = "4";


        await Program.floorRequestListener(floorRequestString, elevator);


        Assert.Equal(Program.downFloorRequests.Count, 1);
    }


    [Fact]
    public async Task TestListenerWithUpwardInput()
    {
        Program.upFloorRequests = new Queue<FloorRequest>();


        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(100.0, 200.0);
        Building building = new Building(numberOfBuildingFloors, elevator);
        elevator.currentFloor = elevator.floors[4];


        string floorRequestString = "8";


        await Program.floorRequestListener(floorRequestString, elevator);


        Assert.Equal(Program.upFloorRequests.Count, 1);
    }

    [Fact]
    public async Task TestListenerWithSameFloorInput()
    {


        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(100.0, 200.0);
        Building building = new Building(numberOfBuildingFloors, elevator);
        elevator.currentFloor = elevator.floors[4];


        string floorRequestString = "4";


        await Program.floorRequestListener(floorRequestString, elevator);


        Assert.Equal(elevator.currentFloor.floorNumber, Int32.Parse(floorRequestString));
    }
}