using UnityEngine;

public class SneezeOnPlayer : PlayerDetector
{
    [SerializeField] private Animator animator;
    protected override void OnFindPlayer(GameObject player)
    {
        animator.SetTrigger("sneeze");
    }
}
