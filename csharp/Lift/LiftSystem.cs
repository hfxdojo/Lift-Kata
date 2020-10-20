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
            if (Calls.Any())
            {
                Lifts[0].CalledFloor = Calls[0].Floor;
            }

            Lifts[0].Tick();

            //Lifts[0].Requests.AddRange(Calls.Select(c => (c.Floor, true)).ToList());

            // I am extending info that goes to the Lift class

            // Calls contain info about LiftSystem
        }

        public void Call(List<Call> calls)
        {
            Calls.AddRange(calls);
            Calls = Calls.Distinct().ToList();
        }
    }
}