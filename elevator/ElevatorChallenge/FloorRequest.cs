using System;
using floorNS;


namespace floorrequestNS
{
    class FloorRequest
    {
        public TimeSpan timeStamp { get; set; }
        public string direction { get; set; }
        public Floor requestedFloor { get; set; }

        public FloorRequest(string floorRequest)
        {
            timeStamp = DateTime.Now.TimeOfDay;
            Console.WriteLine($"{floorRequest}, {timeStamp}");

            if (floorRequest.Length == 2)
            {
                if (floorRequest.Substring(1) == "U")
                {
                    direction = "Up";
                }
                else if (floorRequest.Substring(1) == "D")
                {
                    direction = "Down";
                }
            }
            else
            {
                direction = "N/A";
            }

            requestedFloor = new Floor(Int32.Parse(floorRequest.Substring(0, 1)));
        }

    }
}