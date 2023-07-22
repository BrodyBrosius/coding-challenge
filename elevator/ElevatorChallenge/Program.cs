// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;

using buildingNS;
using elevatorNS;
using floorNS;

namespace ElevatorChallenge
{

    class Program
    {
        static void Main(string[] args)
        {

            Elevator elevator = new Elevator(100.0, 200.0);
            Building building = new Building(10, elevator);


            char userInput = '0';
            string val = "";
            Console.WriteLine("Welcome to the Advanced Elevator Experience!");
            Console.WriteLine("The elevator is presently at floor zero. Please input which floor you would like to go to.");
            Console.WriteLine("If you would like to stop the program, press any letter on your keyboard.");




            while (!Char.IsLetter(userInput))
            {
                val = Console.ReadLine();
                userInput = char.Parse(val);
                Console.WriteLine(userInput);
            }



        }
    }

}





