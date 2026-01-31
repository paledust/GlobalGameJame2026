using UnityEngine;

public class ItemGem : ItemBasic
{
    public override void OnPickUp()
    {
        Debug.Log("Picked up a gem!");
    }
}
