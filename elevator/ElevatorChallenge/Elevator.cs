using floorNS;
using floorrequestNS;
using ElevatorChallenge;
using System.Diagnostics;


namespace elevatorNS
{
    public class Elevator
    {
        public bool isGoingUp = true;
        public bool isGoingDown = false;
        public bool isMoving = false;
        public Floor currentFloor { get; set; }
        public double currentWeight;
        public double weightLimit;
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
                Console.WriteLine($"Elevator is currently at: {this.currentFloor.floorNumber}");
                Console.WriteLine("Elevator is moving...");
                this.isMoving = true;
                if (fr.requestedFloor.floorNumber < this.currentFloor.floorNumber)
                {
                    Floor previousFloor = currentFloor;
                    for (int i = previousFloor.floorNumber; i > fr.requestedFloor.floorNumber; i--)
                    {
                        Task.Delay(3000).Wait();
                        this.currentFloor = this.floors[i];
                        LogElevatorMovement(DateTime.Now.TimeOfDay, this.currentFloor);
                        Console.WriteLine($"Elevator passed floor {i} at {DateTime.Now.TimeOfDay}");

                    }
                }
                else
                {
                    Floor previousFloor = currentFloor;
                    for (int i = previousFloor.floorNumber; i < fr.requestedFloor.floorNumber; i++)
                    {
                        Task.Delay(3000).Wait();
                        this.currentFloor = this.floors[i];
                        LogElevatorMovement(DateTime.Now.TimeOfDay, this.currentFloor);
                        Console.WriteLine($"Elevator passed floor {i} at {DateTime.Now.TimeOfDay}");

                    }
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
                LogElevatorMovement(DateTime.Now.TimeOfDay, this.currentFloor);

                Console.WriteLine($"Elevator is now at: {this.currentFloor.floorNumber}, waiting...");
                Task.Delay(1000).Wait();
            });

        }

        public void LogElevatorMovement(TimeSpan timeStamp, Floor floor)
        {
            string timeStampString = timeStamp.ToString();
            string floorNumberString = floor.floorNumber.ToString();
            if (this.isMoving)
            {

                string elevatorPassingString = $"Elevator passed floor {floorNumberString} at {timeStampString}";

                int numberOfRetries = 100;
                int delayOfRetries = 1000;
                for (int i = 1; i < numberOfRetries; i++)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter("Elevator Movement Log File.txt", true))
                        {
                            File.AppendAllText("Elevator Movement Log File.txt", elevatorPassingString + Environment.NewLine);
                            break;

                        }
                    }
                    catch (IOException e) when (i <= numberOfRetries)
                    {
                        // You may check error code to filter some exceptions, not every error
                        // can be recovered.
                        Thread.Sleep(delayOfRetries);
                    }
                }


            }
            else
            {
                string elevatorArrivedString = $"Elevator arrived at floor {floorNumberString} at {timeStampString}";
                int numberOfRetries = 100;
                int delayOfRetries = 1000;
                for (int i = 1; i < numberOfRetries; i++)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter("Elevator Movement Log File.txt", true))
                        {
                            File.AppendAllText("Elevator Movement Log File.txt", elevatorArrivedString + Environment.NewLine);
                            break;

                        }
                    }
                    catch (IOException e) when (i <= numberOfRetries)
                    {
                        // You may check error code to filter some exceptions, not every error
                        // can be recovered.
                        Thread.Sleep(delayOfRetries);
                    }
                }
            }
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

