using System;
using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class LiftSystem
    {
        public List<int> Floors { get; }
        public List<Lift> Lifts { get; }
        public List<Call> Calls { get; private set; }

        public LiftSystem(List<int> floors, List<Lift> lifts, List<Call> calls)
        {
            Floors = floors;
            Lifts = lifts;
            Calls = calls;
        }

        public List<int> FloorsInDescendingOrder()
        {
            var copy = new List<int>(Floors);
            copy.Reverse();
            return copy;
        }

        public IEnumerable<Call> CallsForFloor(int floor)
        {
            return Calls.Where(c => c.Floor == floor);
        }

        public void Tick()
        {
            var closetToCallLift = Lifts.OrderBy(l => Math.Abs(l.Floor - Calls[0].Floor));
            var closestToCallLift = Lifts.OrderBy(l => Math.Abs(l.Floor - Calls[0].Floor)).First();
            closestToCallLift.CalledFloor = Calls[0].Floor;

            foreach (Lift lift in Lifts) {
           
                lift.Tick();
                
                if (Calls.Any(x => x.Floor == lift.Floor) && lift.DoorsOpen) {
                    Calls.Remove(Calls.Where(x => x.Floor == lift.Floor).First());
                }

                //Lifts[0].Requests.AddRange(Calls.Select(c => (c.Floor, true)).ToList());

                // I am extending info that goes to the Lift class

                // Calls contain info about LiftSystem
            }
        }

        public void Call(List<Call> calls)
        {
            Calls.AddRange(calls);
            Calls = Calls.Distinct().ToList();
        }
    }
}