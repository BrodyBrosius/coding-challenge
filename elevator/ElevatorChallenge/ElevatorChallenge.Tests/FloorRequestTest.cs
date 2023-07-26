using floorNS;
using floorrequestNS;
using Xunit;


namespace ElevatorChallenge.Tests;

public class FloorRequestTest
{
    [Fact]
    public void CreateFloorRequestWithoutDirection()
    {
        string floorInput = "5";

        FloorRequest floorRequest = new FloorRequest(floorInput);

        Assert.Equal(floorRequest.requestedFloor.floorNumber, Int32.Parse(floorInput));
    }

    [Fact]
    public void CreateFloorRequestWithDownDirection()
    {
        string floorInput = "5D";

        FloorRequest floorRequest = new FloorRequest(floorInput);

        Assert.Equal(floorRequest.direction, "Down");
    }

    [Fact]
    public void CreateFloorRequestWithUpDirection()
    {
        string floorInput = "6U";

        FloorRequest floorRequest = new FloorRequest(floorInput);

        Assert.Equal(floorRequest.direction, "Up");
    }
}