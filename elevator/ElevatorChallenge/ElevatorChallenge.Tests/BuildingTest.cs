using elevatorNS;
using buildingNS;
using Xunit;


namespace ElevatorChallenge.Tests;


public class BuildingTest
{
    [Fact]
    public void CreateBuilding()
    {
        double currentWeight = 0.0;
        double maxWeight = 300.0;
        int numberOfBuildingFloors = 10;

        Elevator elevator = new Elevator(currentWeight, maxWeight);
        Building building = new Building(numberOfBuildingFloors, elevator);

        Assert.Equal(building.floors.Count, numberOfBuildingFloors);

    }
}