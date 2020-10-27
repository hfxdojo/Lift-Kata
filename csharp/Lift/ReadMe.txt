# README

Lift features:

[X] A lift fulfills a request when it moves to the requested floor and opens the doors.
[X] A lift fulfills a call when it moves to the correct floor, is about to go in the called direction,
    and opens the doors.
    - We made the decision that we won't try to implement an elevator *direction* at this time
    - Although that could be an interesting future direction
[X] A lift can only move between floors if the doors are closed.

## As of 2020-09-28 -- Last things we did:
- Get the LiftSystem test passing for Call()
- Test that when you make a call, it doesn't add duplicates
- Convert calls to requests for one lift in the system.
- Make sure when calls come in, the LiftSystemPrinter doesn't end up printing them
    as requests

## As of 2020-10-19 -- What we did:
- Got the single lift running
- Started on multiple lifts
- GOAL: wrap it up next session!
    - Try to solve for multiple lifts in simplest way possible
    - Probably: sending call to first lift available instead of ordering by closest?

## As of 2020-10-26 -- what we did:
- we added a test scenario with 2 lifts and 3 calls and we called 2 ticks. 

3 v           3
2 ^           2
1 v        [B]1
0     [A]     0

the first tick() works as expected

3 v        [B]3
2 ^           2
1 v   [A]     1
0             0

after the second tick() this is what we expect to see:

3          ]B[3
2 ^           2
1     ]A[     1
0             0

But instead this happens:

3 v           3
2 ^        [B]2
1     ]A[     1
0             0

It looks like the call assignment is the problem. after the first tick 2 calls are assigned and lifts are moved to the floors but still 
the 2 calls are still in the call list. They are going to be removed only after doors are opened.



Questions:
- Who should be responsible for tracking Calls? LiftSystem or Lift?
    - One option is that LiftSystem uses a scheduler, and Lift is dumb
    - Another is that LiftSystem delegates Calls to a Lift, and 
        Lifts keep track of which Destinations they have: Calls and Requests
	2020-09-28- We decided to try this implementation that lift handles both calls and requests but LiftSystem tracks the calls and pass them to lifts

What was done
- Saeed added a new property to Lift, CalledFloor to let lifts accept a call from LiftSystem
- In this design Saeed proposed to leave the management (dispatch of calls) to LiftSystem
2020-10-05:
- Review the design of Lift and discuss how to go forward with calling lifts
- Decided to focus on fulfilling the specifications only and not designing a world-class perfect Lift

Next up:
- Mark call in LiftSystem as fulfilled => Pass IfCurrentFloorHasACallAndDoorsClosed_OpensDoors (Dmitrii)
    - Maybe change test name? Add "And Clear the Call"? (Saeed)
- Add another lift to the system (Dmitrii)