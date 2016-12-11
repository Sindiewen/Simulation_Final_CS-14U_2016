/*
 * Simulation Final
 *
 * @name  Rachel Vancleave
 * @date  12/7/16
 * @class CS 214U Unity Programming
 *
 */

//////////////////////////////////////////////////////////////////////////////////////////////////////////////

Summary: 

This simulation simulates 5 different actor types acording to their Heartrate (BPM). 
	There are 5 different actor types as followed:

	- Neutral			: Base actor, no preference travel preference, average Heartrate.
			- Gets exhausted at an average pace.
			- Rests for an average time.
			- Likes to hike, so will take a slightly longer route

	- Hiker 			: Likes walking from point A to B. prefers walking on a path that is
		meant for hikers. Has a higher heartrate to compensate for their hiking mentality.
			- Above average heartrate, gets exhausted less.
			- Prefers hikable, not as hard working paths.

	- Body Builder		: Loves taking more hard working paths. Anything to get their heartrate up.
			- Prefers more hard working paths.
			- Has higher than average heartrate. Won't get exhausted as easily.

	- Florist			: Loves beauty and anything that catches ones eyes. Hates working hard and prefers
		easier paths to not raise heartrate.
			- Takes easiest routes to the goal. Anything to not raise the heartrate.
			- Will take detours for anything that might be pretty.

	- Business Worker	: Dont stop, won't stop. WIll take the best path from point A to point B. Although,
		Has an average heartrate, and will go no matter what. Even if it's a harder route.
			- Average heartrate, gets exhausted easily.
			- Get from point A to point B as efficient as possible.
			
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

Question:
	Every actor has a heartrate. As they move, their heartrate increases depending on how far each node is. Every 
Actor is different with different personalities. Their heartrate drives them towards what they can, and can't do.
	
	With each actor having different Idle Heartrate, Max Heartrate they can achieve before exhaustion, and a "Safe"
Heartrate so they can continue moving after cooling down. They need to find the most optimal route to the goal node
with their limitations of their heartrate.

//////////////////////////////////////////////////////////////////////////////////////////////////////////////


How To:

// // // Actor Summary:
Actor Values in the Inspector
	In the Hierarchy, there are 5 actors under the Traveler Holder Game Object. 
	In the inspector, under the Traveler Cotroller Component, each actor has values that can be changed for their AI.

	Actor AI Values:
		- Type		: The type of Actor the actor is.
		- Speed		: How fast the actor moves in the simulation.

	Actor Heartrate Values
		- Age: The actors age.
		- Idle Default BPM: Their default BPM that they'll be at when they're idle.
		- Max BPM: The Max BPM they can achieve before exhaustion.
		- Actor Idle Time: How long in seconds the actor will decrease a BPM.
		- BPM Decrement Value: How many BPM each Actor Idle Time the actor will loose a BPM.

	AI States:
		- Move 		: Checkbox weather the actor moves or not.
		- Start At Start Note: If Unchecked, the actor will start where it currently resides in 3D space,
			and slowly move to the start node.

	Node Locations: - Stores what node the actor will traverse too 
		Start Node 		: What node the actor will start at.
		Goal Node 		: What node the actor will end at.


// // // Preparing the Simulation:
	1) Ensure each actor has a Start Node and a Goal node or else the simulation cannot commence.
	2) Adjust the values for each actor as you see fit.
	3) Hit play.

// // // During the Simulation:
	- You can change the values during the simulation. The actor is able to recalculate their path if you change their
Goal node for example. You could even change the type of actor they are (Florist can become Body Builder, etc...).

	- The goal node is not reccomended to be changed during runtime because with hoe the code is used, it will get overidden
by the current node it's on top of. 

	- On the top right hand corner of the Game window, there are realtime values of each Actors CurrentBPM their MaxBPM,
and their safe BPM so you can track how they're doing before they are exhausted. To the left of it, is a way to 
change the timescale using a slider. In intervals of 0.25 starting at 0.25x, and ending at 3x you can use this to check 
how the actors are doing with slow motion. It does not affect their current speed.



//////////////////////////////////////////////////////////////////////////////////////////////////////////////


Simulation Conclusion:
	I would say, there are more than one conclusions:

Case 1)
	Lets focus on:
		- Each Actor sharing the same movement speed.
		- Each Actor's heartrate values are set to the prefab's default.

	The Start node to node "Node (L 1) - Ragrinar Canyon Path" and the goal node to "Node (U 5) - Believer's Paradise".

	This is our "Default". The entrance to the Bionis Leg Scene is the Start Node, and the goal node is the Highest point
in the map. This is a good test in regards to each actor needs to get out of their comfort zones to approach this goal.

	When they start, they seem to take a similar route, but break up pretty quickly just to get their prefered route.
Once they reach the bridge, they split off again. Neutral, Body Builder and Hiker go into the cave, while the Business Worker
and the Florist go up the path up to the plateau.

	The Florist has to stop and get their BPM to their safe BPM threshold multiple times, and the Business Worker stops less.
The hiker seems to last the longest going up to the goal, and the Body Builder is moving towards the harder routes naturally,
Especially walking up Spiral Valley with the vertical incline that it is.

	The first actor to reach the goal happens to be the Florist Actor. followed up by the Body Builder, then
Business Worker, the Neutral Actor, and lastly the Hiker. Seemingly the slowest, the worst actor for this route got
there first out of the bunch while seemingly the best actor, the Hiker got there last.

Case 1 Conclusion:
	As this being a worst case, the least likley actor won while the most likley actor lost.
	Florist, Body Builder, Business Worker, Neutral, Hiker.


Case 2)
	Keep actor values from Case 1.
	Goal node and Start Node are swapped.

	They all roughly started their decent down the plateau roughly the same say they went up in Case 1.

	The Florist reached the bridge first, but the body builder crossed it first but got exhausted. Followed by 
the Business Worker who also got exhausted. The Florist crossed after and continued going on without getting exhausted.
The neutral actor reached the bridge next while the Florist reached the canyon.

	As the Hiker finally crossed the bridge, the FLorist reached the goal node again. Followed by the Body Builder, then
Business Worker, Neutral and lastly the Hiker.

Case 2 Conclusion:
	Very Similar to Case 1. Again the least likley actor won again:
	Florist, Body Builder, Business Worker, Neutral, Hiker.


Case 3)
	Two new start and goal nodes, but keep actor values the same.
	Start Node is "Node (L) - Rho Oasis" and the Goal node is "Node - Upper Spiral Valley"

	This test would do more of a straight climb, Especially going straight up at Spiral Valley. All 5 actors reached the
bridge at roughly the same time. Each one of them were in a line one after another. The line went in order of
Hiker, With body Builder and Neutral follwing up next, then Florist and lastly Business Worker.

	The hiker and Body Builder went into the caves to come at Spiral Valley from the bottom while the rest took the path
instead. The Hiker got exhausted first just before the pillar, followed up the Business Worker, then the Florist and
neutral got exhausted, And lastly the body builder got exhausted half way up. The body builder has a long wait before they
can continue moving again.

	Neutral and Florist continued, but got exhausted along the way. Neutral and Body Builder share the same location now,
but shortly after neutral became exhausted, Body Builder arrived at the goal. Followed by Neutral, Florist, Business Worker
and lastly, the hiker arriving last, but taking their time.

	The Hiker decided not to climb spiral valley and instead, take the longer path around in the caves to get up there.
Again, the Hiker got there last because of it.

Case 2 Conclusion:
	I expected the Body Builder to arrive at the goal node first, and I was expecting the Hiker to arrive next too. I
was pleasently suprised the Hiker didn't, and it decided to take the longer way around. 
	Body Builder, Neutral, Florist, Business Worker, Hiker.



Overall Simulation Conclusion:

	The actors seem to act the way I expected to. Neutral taking the base route given by Dijkstra, Hiker taking the more
longer route, Body builder taking the harder route, Florist taking the easiest route, and Business Worker taking the
direct route. With that said, a few things I did not expect popped up.

	In case 1, the Florist won. I was eexpecting the Business Worker to win with it's personality to primarily get from
point A to B in the most direct route. In fact, the Florist won when I thought It was going to loose with it getting more
exhausted faster with their lower Max BPM. The Hiker did as I expected.

	In case 2, Again, just like Case 1, the Florist won, with everyone else getting there in the same order as the
previous test. Not much of a change. 

	Case 3 though was the most interesting. I expected the Body Builder to win, but I did not expect the Hiker to take
dead last with it arriving at the goal quite some time later. I was pleasently suprised, but at the same time, I was 
not expecting the Hiker to arrive last to the goal thinking it could climb Spiral Valley.

	I would say though, this did solve my question of sinding the most optimal path's in regards to heartrate and finding
the most optimal path for each Actor. Each actor has a separate path they use when going towards the goal node. There are
some simularities, but each take a different route, and each route is unique to them.