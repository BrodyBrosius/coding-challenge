
namespace elevatorNS
{
    class Elevator
    {
        bool isGoingUp = false;
        bool isGoingDown = false;
        bool isMoving = false;
        int currentFloor = 0;
        double currentWeight;
        double weightLimit;
        public Elevator(double currentWeight, double weightLimit)
        {
            this.currentWeight = currentWeight;
            this.weightLimit = weightLimit;
        }
    }
}

