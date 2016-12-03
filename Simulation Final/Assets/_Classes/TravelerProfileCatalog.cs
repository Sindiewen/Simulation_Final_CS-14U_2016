using System;
using System.Collections.Generic;

public class TravelerProfileCatalog
{
	public enum TravelerType
    {
        Neutral,            // Neutral Actor    - Base
        Hiker,              // Hiker            - Actor likes climbing
        BodyBuilder,        // BodyBuilder      - Actor likes working hard
        Florist,            // Florist          - Actor prefers beauty (change to florist)
        BusinessWorker,     // BusinessWorker   - Actor takes more direct route
    };
	
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
		//////////////////////////////////////////////
        // Neutral Actor: Base case
		if (!profileDict.ContainsKey (TravelerType.Neutral))
		{
			profileDict[TravelerType.Neutral] = new TravelerProfile ();
		}

        // Hiker
        if (!profileDict.ContainsKey (TravelerType.Hiker))
		{
			profileDict[TravelerType.Hiker] = new TravelerProfile (1f, 0f, 0.25f, -1f);
		}

        // Body Builder
        if (!profileDict.ContainsKey (TravelerType.BodyBuilder))
        {
            profileDict[TravelerType.BodyBuilder] = new TravelerProfile();
        }

        // Florist (Change from heart to florist)
		if (!profileDict.ContainsKey (TravelerType.Florist))
		{
			profileDict[TravelerType.Florist] = new TravelerProfile (0.25f, -1f, 1f, -1f);
		}

        // Business Worker
        if (!profileDict.ContainsKey (TravelerType.BusinessWorker))
        {
            profileDict[TravelerType.BusinessWorker] = new TravelerProfile();
        }
		//////////////////////////////////////////////
	}
}