using UnityEngine;
using System;

public class TravelerProfile
{
    // TODO: Modify traveler profile to incorporate:
    // Beauty, Hiker, Body Builder, Business Worker
    /*
	[Range(-1f,1f)]public float LikesToWalk = 0f;       // DEPRICATED
	[Range(-1f,1f)]public float LikesToBicycle = 0f;    // DEPRICATED?
	[Range(-1f,1f)]public float LikesToDrive = 0f;      // DEPRICATED
	[Range(-1f,1f)]public float LikesVistas = 0f;       // Beauty
	[Range(-1f,1f)]public float LikesFood = 0f;         // DEPRICATED
    */


	// Traveler profile: Actors heartrate
    [Range(-1f,1f)]public float LikesHiking         = 0f;
	[Range(-1f,1f)]public float LikesBodyBuilding   = 0f;
    [Range(-1f,1f)]public float LikesBeauty         = 0f;
    [Range(-1f,1f)]public float LikesBusiness       = 0f;


	public TravelerProfile ()
	{
	}

    /*
	public TravelerProfile(float walk, float bike, float car, float vistas, float food, float heart)
	{
		LikesToWalk = walk;
		LikesToBicycle = bike;
		LikesToDrive = car;
		LikesVistas = vistas;       // Beauty
		LikesFood = food;

		// Traveler constructor: Heartrate
		LikesHeart = heart;
	}*/

    public TravelerProfile(float hike, float body, float flower, float business)
    {
        LikesHiking = hike;
        LikesBodyBuilding = body;
        LikesBeauty = flower;
        LikesBusiness = business;
    }
}