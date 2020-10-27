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
            int callIndex=0;

            foreach (Lift lift in Lifts) {
                
                if(Calls.Any() && callIndex < Calls.Count())
                    lift.CalledFloor = Calls[callIndex++].Floor;


                lift.Tick();
                
                if (Calls.Any(x => x.Floor == lift.Floor) && lift.DoorsOpen) {
                    Calls.Remove(Calls.Where(x => x.Floor == lift.Floor).First());
                }
            }
        }

        public void Call(List<Call> calls)
        {
            Calls.AddRange(calls);
            Calls = Calls.Distinct().ToList();
        }
    }
}