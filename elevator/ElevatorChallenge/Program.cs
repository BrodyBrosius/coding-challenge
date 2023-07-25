﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Diagnostics;

using buildingNS;
using elevatorNS;
using floorNS;
using floorrequestNS;

namespace ElevatorChallenge
{

    class Program
    {
        public static Queue<FloorRequest> upFloorRequests;
        public static Queue<FloorRequest> downFloorRequests;

        static async Task Main(string[] args)
        {
            Program.upFloorRequests = new Queue<FloorRequest>();
            Program.downFloorRequests = new Queue<FloorRequest>();

            Elevator elevator = new Elevator(100.0, 200.0);
            Building building = new Building(10, elevator);

            GenerateElevatorLogFile();


            string val = "";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Advanced Elevator Experience!");
            Console.WriteLine("The elevator is presently at floor zero. Please input which floor you would like to go to.");
            Console.WriteLine("If you would like to stop the program, press 'q' on your keyboard.");


            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                string readLineTask;
                readLineTask = await GetInputAsync();
                //Console.WriteLine($"Input: {readLineTask}. Calling listener...");
                Debug.WriteLine($"Input value: {readLineTask}");
                if (floorRequestValidator(readLineTask) == true)
                {
                    await floorRequestListener(readLineTask, elevator);
                    await elevator.fetchNewFloorRequest();
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                }

            }



        }

        public static void GenerateElevatorLogFile()
        {
            string fileName = "Elevator Log File.txt";

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        public static async Task<string> GetInputAsync()
        {
            Debug.WriteLine($"Recieved request at {DateTime.Now.TimeOfDay}");
            return await Task.Run(() => Console.ReadLine());
        }

        public static bool floorRequestValidator(string floorRequest)
        {
            if (floorRequest.Length > 2)
            {
                Console.WriteLine($"Request for {floorRequest} denied. Input too long.");
                return false;
            }
            else if (floorRequest.Length < 1)
            {
                Console.WriteLine($"Request for {floorRequest} denied. Input too short.");
                return false;
            }
            return true;
        }
        public static async Task floorRequestListener(string floorRequest, Elevator elevator)
        {
            await Task.Run(async () =>
            {
                Console.WriteLine($"Request for {floorRequest} recieved...");
                FloorRequest newRequest = new(floorRequest);
                if (elevator.isMoving == true)
                {
                    if (elevator.isGoingUp)
                    {
                        downFloorRequests.Enqueue(newRequest);
                    }
                    if (elevator.isGoingDown)
                    {
                        upFloorRequests.Enqueue(newRequest);
                    }
                }
                if (newRequest.requestedFloor.floorNumber < elevator.currentFloor.floorNumber)
                {
                    newRequest.direction = "Down";
                    downFloorRequests.Enqueue(newRequest);

                }
                else if (newRequest.requestedFloor.floorNumber > elevator.currentFloor.floorNumber)
                {
                    newRequest.direction = "Up";
                    upFloorRequests.Enqueue(newRequest);
                }
                else
                {
                    await elevator.moveElevatorToGivenFloor(newRequest);
                }
            });

        }
    }

}