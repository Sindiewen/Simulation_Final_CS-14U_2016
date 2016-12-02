using UnityEngine;
using System;

public class TravelerProfile
{
	[Range(-1f,1f)]public float LikesToWalk = 0f;
	[Range(-1f,1f)]public float LikesToBicycle = 0f;
	[Range(-1f,1f)]public float LikesToDrive = 0f;
	[Range(-1f,1f)]public float LikesVistas = 0f;
	[Range(-1f,1f)]public float LikesFood = 0f;


	// Traveler profile: Actors heartrate
	[Range(-1f,1f)]public float LikesHeart = 0f;


	public TravelerProfile ()
	{
	}

	public TravelerProfile(float walk, float bike, float car, float vistas, float food, float heart)
	{
		LikesToWalk = walk;
		LikesToBicycle = bike;
		LikesToDrive = car;
		LikesVistas = vistas;
		LikesFood = food;

		// Traveler constructor: Heartrate
		LikesHeart = heart;
	}
}