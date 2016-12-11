using UnityEngine;
using System;

public class TravelerProfile
{
	// Traveler profile: Actors heartrate
    [Range(-1f,1f)]public float LikesHiking         = 0f;
	[Range(-1f,1f)]public float LikesBodyBuilding   = 0f;
    [Range(-1f,1f)]public float LikesBeauty         = 0f;
    [Range(-1f,1f)]public float LikesBusiness       = 0f;


    // Default Constructor
	public TravelerProfile ()
	{
	}

    // Constructor
    public TravelerProfile(float hike, float body, float flower, float business)
    {
        LikesHiking = hike;
        LikesBodyBuilding = body;
        LikesBeauty = flower;
        LikesBusiness = business;
    }
}