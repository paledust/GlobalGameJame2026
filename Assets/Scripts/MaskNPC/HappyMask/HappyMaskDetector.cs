using UnityEngine;

public class HappyMaskDetector : PlayerDetector
{
    [SerializeField] private HappyMask happyMask;
    protected override void OnFindPlayer(GameObject player)
    {
        happyMask.OnFindPlayer(player);
    }
}
