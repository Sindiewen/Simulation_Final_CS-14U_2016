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

	- On the top right hand corner of the Game window, there are realtime values of each Actors CurrentBPM and their MaxBPM
so you can track how they're doing before they are exhausted. To the left of it, is 