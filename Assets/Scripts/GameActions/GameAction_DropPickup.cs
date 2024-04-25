using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction_DropPickup : GameAction
{
    public WeightedRandom.WeightedItem[] dropTable;

    public void DropRandomItem()
    {
        if (dropTable != null)
        {
            Instantiate(WeightedRandom.GetRandomItem(dropTable), transform.position, transform.rotation);
        }
    }

}