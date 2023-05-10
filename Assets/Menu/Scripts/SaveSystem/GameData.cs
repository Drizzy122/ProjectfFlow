using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    //public int deathCount;

    
    public SerializableDictionary<string, bool> coinsCollected;
    //Optional remove if not needed
    //public AttributesData playerAttributesData;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
       // this.deathCount = 0;
        
        coinsCollected = new SerializableDictionary<string, bool>();
        
    } 

    public int GetPercentageComplete()
    {
        // figure out how manu coins we've collected
        int totalCollected = 0; ;
        foreach (bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        // ensure we don't divide by 0 when calculating the percentage
        int percentageComplete = -1;
        if (coinsCollected.Count != 0)
        {
            percentageComplete = (totalCollected * 100 / coinsCollected.Count);
        }
        return percentageComplete;
    }
}