
namespace elevatorNS
{
    class Elevator
    {
        bool isGoingUp = false;
        bool isGoingDown = false;
        bool isMoving = false;
        int currentFloor = 0;
        int nextFloor = -1;
        double currentWeight;
        double weightLimit;
        public Elevator(double currentWeight, double weightLimit)
        {
            this.currentWeight = currentWeight;
            this.weightLimit = weightLimit;
        }

        public void moveElevatorByNumOfFloors()
        {

        }

        public void floorListener(int newFloorNumber)
        {

        }
    }
}

