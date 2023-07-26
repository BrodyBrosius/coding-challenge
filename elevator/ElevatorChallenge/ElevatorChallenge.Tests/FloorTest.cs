using floorNS;
using Xunit;


namespace ElevatorChallenge.Tests;


public class FloorTest
{
    [Fact]
    public void CreateFloorGivenInt()
    {
        int testFloorNumber = 5;
        Floor testFloor = new Floor(testFloorNumber);

        Assert.Equal(testFloor.floorNumber, testFloorNumber);
    }
}