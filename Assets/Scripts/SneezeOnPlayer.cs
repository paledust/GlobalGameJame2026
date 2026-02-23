using UnityEngine;

public class SneezeOnPlayer : PlayerDetector
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem sneezeVFX;
    protected override void OnFindPlayer(GameObject player)
    {
        sneezeVFX.Play();
        animator.SetTrigger("sneeze");
    }
}
