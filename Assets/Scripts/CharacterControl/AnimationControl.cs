using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private static readonly int HAPPY_TRIGGER_HASH = Animator.StringToHash("happy");
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TriggerHappy()
    {
        animator.SetTrigger(HAPPY_TRIGGER_HASH);
    }
}
