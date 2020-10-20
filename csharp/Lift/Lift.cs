using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class Lift
    {
        public string Id { get; private set; }
        public int Floor { get; private set; }
        public List<int> Requests { get; private set; }
        public bool DoorsOpen { get; private set; }
        public int CalledFloor { get; set; }
        private List<int> CallAndRequests
        {
            get
            {
                var callAndRequestsList = Requests.GetRange(0, Requests.Count);
                if (CalledFloor != Int32.MaxValue)
                {
                    callAndRequestsList.Add(CalledFloor);
                }
                return callAndRequestsList;
            }
        }

        public Lift(string id, int floor, List<int> requests, bool doorsOpen = false)
        {
            Id = id;
            Floor = floor;
            Requests = requests;
            DoorsOpen = doorsOpen;

            CalledFloor = Int32.MaxValue;
        }

        public Lift(string id, int floor, bool doorsOpen = false) : this(id, floor, new List<int>(), doorsOpen)
        {
        }

        void Move(int floor)
        {
            if (this.Floor != floor){
                this.Floor = floor;
            }
        }

        public bool HasRequestForFloor(int floor)
        {
            return Requests.Contains(floor);
        }

        public bool HasCallForFloor(int floor)
        {
            return (CalledFloor == floor);
        }

        private void MarkRequestCompleted(){
            Requests.Remove(this.Floor);
        }

        private void OpenDoor(){
            if (!DoorsOpen){
                DoorsOpen = true;

                if (CalledFloor == this.Floor)
                {
                    CalledFloor = Int32.MaxValue;
                }

                MarkRequestCompleted();
            }
        }

        private void CloseDoor()
        {
            if (DoorsOpen)
                DoorsOpen = false;
        }

        private int FindNextFloor()
        {
            return CallAndRequests.OrderBy(Item => Math.Abs(Item - this.Floor)).First();
        }

        public void Tick()
        {
            if ((this.HasRequestForFloor(this.Floor) || this.HasCallForFloor(this.Floor)) && !this.DoorsOpen)
            {
                OpenDoor();
                return;
            }

            if (!CallAndRequests.Contains(this.Floor) && CallAndRequests.Count > 0 && this.DoorsOpen)
            {
                CloseDoor();
                return;
            }

            if (!CallAndRequests.Contains(this.Floor) && CallAndRequests.Count > 0 && !this.DoorsOpen)
            {
                int nextFloor = FindNextFloor();
                Move(nextFloor);
                return;
            }
        }
    }
}
