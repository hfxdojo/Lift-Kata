using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace Lift
{
    public class LiftSystemTest
    {
        // TODO: enable this test and finish writing it
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void NoCall_StaysTheSame()
        {
            var liftA = new Lift("A", 0);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfCurrentFloorIsRequestedAndDoorsClosed_OpensDoors()
        {
            var liftA = new Lift("A", 0, new List<int> { 0 }, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfCurrentFloorHasACallAndDoorsClosed_OpensDoors()
        {
            var liftA = new Lift("A", 0, new List<int> { }, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call> { new Call(0, Direction.Up) });

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfTwoRequests_GoToClosestFloor()
        {
            var liftA = new Lift("A", 1, new List<int> { 3, 2 }, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call> { });

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        //[Fact]
        //[UseReporter(typeof(DiffReporter))]
        //public void IfThreeRequests_GoToClosestFloor()
        //{
        //    var liftA = new Lift("A", 0, new List<int> { 2,3 }, false);
        //    var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call> { });

        //    lifts.Tick();

        //    Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        //}

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfAnotherFloorIsRequestedAndDoorsOpen_CloseDoors()
        {
            var liftA = new Lift("A", 0, new List<int> { 1 }, true);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfAnotherFloorIsRequestedAndDoorsClosed_MoveLift()
        {
            var liftA = new Lift("A", 0, new List<int> { 1 }, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void LiftSystemAcceptsCalls()
        {
            var liftA = new Lift("A", 0, new List<int> {}, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());

            lifts.Call(new List<Call> { new Call(3, Direction.Down) });

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void LiftSystemCalls_DoNotCreateDuplicates()
        {
            var liftA = new Lift("A", 0, new List<int> {}, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());


            lifts.Call(new List<Call> { new Call(3, Direction.Down) });
            lifts.Call(new List<Call> { new Call(3, Direction.Down) });

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }

        //[Fact]
        //[UseReporter(typeof(DiffReporter))]
        //public void Lift_GetsRequest_WhenCalled()
        //{
        //    // Assumption: Lift.Move() should be private (Dmitrii)

        //    var liftA = new Lift("A", 0, new List<int> { }, false);
        //    var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call>());


        //    lifts.Call(new List<Call> { new Call(3, Direction.Down) });

        //    lifts.Tick();

        //    Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        //}

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void IfThereIsCallFromAnotherFloorAndDoorsClosed_MoveLift()
        {
            var liftA = new Lift("A", 0, new List<int> { }, false);
            var lifts = new LiftSystem(new List<int>() { 0, 1, 2, 3 }, new List<Lift> { liftA }, new List<Call> { new Call(1, Direction.Up) });

            lifts.Tick();

            Approvals.Verify(new LiftSystemPrinter().Print(lifts));
        }
    }
}