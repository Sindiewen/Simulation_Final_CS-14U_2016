using System;
using System.Collections.Generic;

public class TravelerProfileCatalog
{
	public enum TravelerType { Bicyclist, CityDriver, Hiker, Neutral, VacationDriver, Heart };
	
	private static Dictionary<TravelerType,TravelerProfile> profileDict = new Dictionary<TravelerType, TravelerProfile>();

	public static TravelerProfile GetProfile(TravelerType type)
	{
		TravelerProfile value = null;
		if (profileDict.ContainsKey (type))
		{
			value = profileDict [type];
		}
		// identical to:
		// profileDict.TryGetValue (type, out value);
		return value;
	}

	public static void Initialize()
	{
		if (!profileDict.ContainsKey (TravelerType.Bicyclist))
		{
			profileDict [TravelerType.Bicyclist] = new TravelerProfile (0f, 1f, -1f, 0.25f, 0.5f, 0f);
		}
		if (!profileDict.ContainsKey (TravelerType.CityDriver))
		{
			profileDict [TravelerType.CityDriver] = new TravelerProfile (0f, -1f, 1f, 0f, 0f, 0f);
		}
		if (!profileDict.ContainsKey (TravelerType.Hiker))
		{
			profileDict [TravelerType.Hiker] = new TravelerProfile (1f, 0f, -1f, 1f, 0.5f, 0f);
		}
		if (!profileDict.ContainsKey (TravelerType.Neutral))
		{
			profileDict [TravelerType.Neutral] = new TravelerProfile ();
		}
		if (!profileDict.ContainsKey (TravelerType.VacationDriver))
		{
			profileDict [TravelerType.VacationDriver] = new TravelerProfile (0f, 0f, 0.75f, 1f, 1f, 0f);
		}




		//////////////////////////////////////////////
		if (!profileDict.ContainsKey (TravelerType.Heart))
		{
			profileDict [TravelerType.Heart] = new TravelerProfile (0f, 0f, 0f, 0f, 0f, -1f);
		}
		//////////////////////////////////////////////
	}

	private TravelerProfileCatalog ()
	{
	}
}