using System;
using floorNS;


namespace floorrequestNS
{
    class FloorRequest
    {
        TimeSpan timeStamp { get; set; }
        string direction { get; set; }
        Floor requestedFloor { get; set; }

        public FloorRequest(string floorRequest)
        {
            this.timeStamp = DateTime.Now.TimeOfDay;
            Console.WriteLine($"{floorRequest}, {timeStamp}");

            if (floorRequest.Length == 2)
            {
                if (floorRequest.Substring(1) == "U")
                {
                    this.direction = "Up";
                }
                else if (floorRequest.Substring(1) == "D")
                {
                    this.direction = "Down";
                }
            }
            else
            {
                this.direction = "N/A";
            }

            this.requestedFloor = new Floor(Int32.Parse(floorRequest.Substring(0)));
        }

    }
}