using floorNS;
using floorrequestNS;
using ElevatorChallenge;
using System.Diagnostics;


namespace elevatorNS
{
    class Elevator
    {
        public bool isGoingUp = true;
        public bool isGoingDown = false;
        public bool isMoving = false;
        public Floor currentFloor { get; set; }
        int nextFloor = 0;
        double currentWeight;
        double weightLimit;
        public List<Floor> floors { get; set; }
        public Elevator(double currentWeight, double weightLimit)
        {
            this.currentWeight = currentWeight;
            this.weightLimit = weightLimit;
            this.floors = null;
        }

        public async Task moveElevatorToGivenFloor(FloorRequest fr)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine($"Elevator is currently at: {this.currentFloor.floorNumber}");
                Debug.WriteLine("Elevator is moving...");
                this.isMoving = true;
                Floor previousFloor = currentFloor;
                for (int i = previousFloor.floorNumber; i < fr.requestedFloor.floorNumber; i++)
                {
                    Task.Delay(3000).Wait();
                    this.currentFloor = this.floors[i];
                    Debug.WriteLine($"Elevator passed floor {i} at {DateTime.Now.TimeOfDay}");
                }
                Task.Delay(3000).Wait();
                int floorReqValue = fr.requestedFloor.floorNumber;
                this.currentFloor = this.floors[floorReqValue];
                if (this.currentFloor.floorNumber == this.floors.Count - 1)
                {
                    this.isGoingDown = true;
                    this.isGoingUp = false;
                }
                else if (this.currentFloor.floorNumber == 0)
                {
                    this.isGoingDown = false;
                    this.isGoingUp = true;
                }
                this.isMoving = false;
                Debug.WriteLine($"Elevator arrived at {this.currentFloor.floorNumber}, time is {DateTime.Now.TimeOfDay}");
                Debug.WriteLine($"Elevator is now at: {this.currentFloor.floorNumber}, waiting...");
                Task.Delay(1000).Wait();
            });

        }

        public async Task fetchNewFloorRequest()
        {

            if (Program.upFloorRequests.Count == 0 && Program.downFloorRequests.Count == 0)
            {
                Console.WriteLine("No requests!");
            }
            else
            {
                await Task.Run(async () =>
                {
                    while (this.isGoingUp && Program.upFloorRequests.Count != 0)
                    {
                        await moveElevatorToGivenFloor(Program.upFloorRequests.Dequeue());
                    }

                    while (this.isGoingDown && Program.downFloorRequests.Count != 0)
                    {
                        await moveElevatorToGivenFloor(Program.downFloorRequests.Dequeue());
                    }

                });
            }
        }
    }
}

