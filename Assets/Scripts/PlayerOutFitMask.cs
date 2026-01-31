using UnityEngine;

public class PlayerOutFitMask : PlayerDetector
{
    protected override void OnFindPlayer(GameObject player)
    {
        player.GetComponent<PlayerMaskController>().ChangeMask();
    }
}
