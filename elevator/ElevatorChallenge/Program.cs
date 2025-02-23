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

    public class Program
    {
        public static Queue<FloorRequest> upFloorRequests;
        public static Queue<FloorRequest> downFloorRequests;

        static async Task Main(string[] args)
        {
            Program.upFloorRequests = new Queue<FloorRequest>();
            Program.downFloorRequests = new Queue<FloorRequest>();

            Elevator elevator = new Elevator(100.0, 200.0);
            Building building = new Building(10, elevator);

            //GenerateElevatorLogFile();
            //GenerateElevatorRequestFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Advanced Elevator Experience!");
            Console.WriteLine("The elevator is presently at floor zero. Please input which floor you would like to go to.");
            Console.WriteLine("If you would like to stop the program, press 'q' on your keyboard.");

            bool userRequestedEndToProgram = false;


            while (userRequestedEndToProgram == false)
            {
                string readLineTask;
                readLineTask = await GetInputAsync();
                if (readLineTask == "Q" || readLineTask == "q")
                {
                    userRequestedEndToProgram = true;
                    break;
                }
                Debug.WriteLine($"Input value: {readLineTask}");
                LogRequest(readLineTask);
                if (floorRequestValidator(readLineTask) == true)
                {
                    await floorRequestListener(readLineTask, elevator);
                    await elevator.fetchNewFloorRequest();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Invalid entry");
                }

            }

            Console.WriteLine("PROGRAM END");


        }
        public static void LogRequest(string request)
        {
            TimeSpan timeStamp = DateTime.Now.TimeOfDay;
            string reqTimeStamp = timeStamp.ToString();
            string log = $"Request Content: {request}, request made at {reqTimeStamp}.";
            int numberOfRetries = 100;
            int delayOfRetries = 1000;
            for (int i = 1; i < numberOfRetries; i++)
            {
                try
                {
                    using StreamWriter writer = new("Elevator Request Log File.txt", true);
                    writer.Close();
                    File.AppendAllText("Elevator Request Log File.txt", log + Environment.NewLine);
                    
                    break;
                }
                catch (IOException e) when (i <= numberOfRetries)
                {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Thread.Sleep(delayOfRetries);
                }
            }
        }
        public static async Task<string> GetInputAsync()
        {
            return await Task.Run(() => Console.ReadLine());
        }

        public static bool floorRequestValidator(string floorRequest)
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.ForegroundColor = ConsoleColor.Green;
            await Task.Run(async () =>
            {
                Console.WriteLine($"Request for {floorRequest} recieved...");
                FloorRequest newRequest = new(floorRequest);



                if (newRequest.requestedFloor.floorNumber < elevator.currentFloor.floorNumber || newRequest.direction == "Down")
                {
                    newRequest.direction = "Down";
                    downFloorRequests.Enqueue(newRequest);

                }
                else if (newRequest.requestedFloor.floorNumber > elevator.currentFloor.floorNumber || newRequest.direction == "Up")
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