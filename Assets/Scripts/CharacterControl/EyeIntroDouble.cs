using UnityEngine;

public class EyeIntroDouble : Eye
{
    [SerializeField] private Animation eyeMask;
    protected override void OnCloseEye()
    {
        eyeMask.Play("intro_close");
    }
    protected override void OnOpenEye()
    {
        eyeMask.Play("intro_open");
    }
}
