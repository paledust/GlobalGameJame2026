using UnityEngine;

public class PlayerEnterZone : PlayerDetector
{
    [SerializeField] private ShyMaskController shyMaskController;
    protected override void OnFindPlayer(GameObject player)
    {
        shyMaskController.StartShyMaskAppearance();
        Destroy(gameObject);
    }
}
