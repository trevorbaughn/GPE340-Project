using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class WeightedRandom
{
    public static GameObject GetRandomItem(WeightedItem[] possibleItems)
    {
        //make CDA from possibleitems.
        //it is an array of the item weights
        float[] CDA = new float[possibleItems.Length];
        float cumulativeDensity = 0;
        for (int i = 0; i < possibleItems.Length; i++)
        {
            // Add the density of next possible item to the total
            cumulativeDensity += possibleItems[i].weight;

            //set that index weight to that total
            CDA[i] = cumulativeDensity;
        }

        float randomValue = Random.Range(0, cumulativeDensity); // get random value between the cumulative item weight & 0

        // find in CDA
        int selectedIndex = System.Array.BinarySearch(CDA, randomValue);
        if (selectedIndex < 0)
        {
            // if selectedIndex is negative, the exact randomValue was not found
            // using a bitwise NOT, we can turn it positive+1, which works great
            selectedIndex = ~selectedIndex;
        }
        return possibleItems[selectedIndex].objectToDrop; 
    }

    [System.Serializable]
    public class WeightedItem
    {
        public GameObject objectToDrop;
        public float weight;
    }
}
