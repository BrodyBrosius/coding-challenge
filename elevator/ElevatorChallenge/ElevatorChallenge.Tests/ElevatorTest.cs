using elevatorNS;
using floorrequestNS;
using floorNS;
using buildingNS;
using Xunit;
using System;
using System.Diagnostics;
using ElevatorChallenge;

namespace ElevatorChallenge.Tests;

public class ElevatorTest
{
    [Fact]
    public void CreateElevatorGivenCurrentAndMaxWeight()
    {
        double currentWeight = 0.0;
        double maxWeight = 250.0;

        Elevator elevator = new Elevator(currentWeight, maxWeight);

        Assert.Equal(currentWeight, elevator.currentWeight);
    }

    [Fact]
    public async Task MoveElevatorUp()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);


        string incomingFloor = "4";
        FloorRequest floorReq = new FloorRequest(incomingFloor);

        await elevator.moveElevatorToGivenFloor(floorReq);

        Assert.Equal(elevator.currentFloor.floorNumber, floorReq.requestedFloor.floorNumber);


    }

    [Fact]
    public async Task MoveElevatorDown()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);

        elevator.currentFloor = elevator.floors[7];

        string incomingFloor = "5";
        FloorRequest floorReq = new FloorRequest(incomingFloor);

        await elevator.moveElevatorToGivenFloor(floorReq);

        Assert.Equal(elevator.currentFloor.floorNumber, floorReq.requestedFloor.floorNumber);


    }

    [Fact]
    public async Task MoveElevatorTopFloor()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);

        string incomingFloor = "9";
        FloorRequest floorReq = new FloorRequest(incomingFloor);

        await elevator.moveElevatorToGivenFloor(floorReq);

        Assert.Equal(elevator.currentFloor.floorNumber, building.floors.Count - 1);


    }


    [Fact]
    public async Task MoveElevatorBottomFloor()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);

        elevator.currentFloor = elevator.floors[9];


        string incomingFloor = "0";
        FloorRequest floorReq = new FloorRequest(incomingFloor);

        await elevator.moveElevatorToGivenFloor(floorReq);

        Assert.Equal(elevator.currentFloor.floorNumber, 0);


    }

    [Fact]
    public async Task CompleteMultipleStops()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);

        elevator.currentFloor = elevator.floors[1];


        Program.upFloorRequests = new Queue<FloorRequest>();
        Program.downFloorRequests = new Queue<FloorRequest>();


        FloorRequest upReqOne = new FloorRequest("2");
        FloorRequest upReqTwo = new FloorRequest("4");
        FloorRequest upReqThree = new FloorRequest("9");

        Program.upFloorRequests.Enqueue(upReqOne);
        Program.upFloorRequests.Enqueue(upReqTwo);
        Program.upFloorRequests.Enqueue(upReqThree);

        FloorRequest downReqOne = new FloorRequest("8");
        FloorRequest downReqTwo = new FloorRequest("3");
        FloorRequest downReqThree = new FloorRequest("0");

        Program.downFloorRequests.Enqueue(downReqOne);
        Program.downFloorRequests.Enqueue(downReqTwo);
        Program.downFloorRequests.Enqueue(downReqThree);




        while (Program.upFloorRequests.Count > 0 && Program.downFloorRequests.Count > 0)
        {
            await elevator.fetchNewFloorRequest();
        }

        Assert.Equal(elevator.currentFloor.floorNumber, 0);
    }
}