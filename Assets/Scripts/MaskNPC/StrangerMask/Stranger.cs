using UnityEngine;

public class Stranger : MonoBehaviour
{
    [SerializeField] private AnimationControl animationControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SmellFlower()
    {
        animationControl.TriggerHappy();
    }
}
