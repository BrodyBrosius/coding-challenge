// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;

using buildingNS;
using elevatorNS;
using floorNS;
using floorrequestNS;

namespace ElevatorChallenge
{

    class Program
    {
        static void Main(string[] args)
        {

            Elevator elevator = new Elevator(100.0, 200.0);
            Building building = new Building(10, elevator);


            string userInput = "";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Advanced Elevator Experience!");
            Console.WriteLine("The elevator is presently at floor zero. Please input which floor you would like to go to.");
            Console.WriteLine("If you would like to stop the program, press any letter on your keyboard.");

            Console.ForegroundColor = ConsoleColor.White;
            bool go = true;
            while (go)
            {
                string val = Console.ReadLine();
                Console.WriteLine($"Input: {val}. Calling listener...");
                if (floorRequestValidator(val) == true)
                {
                    floorRequestListener(val);
                }
                else
                {
                    go = false;
                }

            }



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
        public static async Task floorRequestListener(string floorRequest)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Request for {floorRequest} recieved...");
                FloorRequest newRequest = new FloorRequest(floorRequest);
                Console.WriteLine($"TimeStamp for {floorRequest}: {newRequest.timeStamp}");
                Task.Delay(5000).Wait();
            });

        }
    }

}





