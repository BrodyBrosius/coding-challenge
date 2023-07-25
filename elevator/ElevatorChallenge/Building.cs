using floorNS;
using elevatorNS;

namespace buildingNS
{
    class Building
    {
        int numberOfFloors;
        public List<Floor> floors = new List<Floor>();
        Elevator elevator;

        public Building(int numberOfFloors, Elevator elevator)
        {
            this.numberOfFloors = numberOfFloors;
            this.elevator = elevator;


            for (int i = 0; i < numberOfFloors; i++)
            {
                Floor currentFloor = new Floor(i);
                this.floors.Add(currentFloor);
            }

            elevator.floors = floors;
            elevator.currentFloor = floors[0];

        }

        public int getBuildingStories()
        {
            return this.floors.Count;
        }
    }
}





