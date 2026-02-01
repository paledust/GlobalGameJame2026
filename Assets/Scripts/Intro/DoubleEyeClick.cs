using UnityEngine;

public class DoubleEyeClick : MonoBehaviour
{
    [SerializeField] private Eye[] eyes;
    [SerializeField] private int triggerCount = 4;
    private int blinkIndex = 0;
    private bool isEnded = false;
    void Start()
    {
        EventHandler.E_OnBlinkEye += SwichEyeMask;
    }
    void OnDestroy()
    {
        EventHandler.E_OnBlinkEye -= SwichEyeMask;   
    }
    void SwichEyeMask()
    {
        blinkIndex ++;
        blinkIndex = blinkIndex % 3;
        foreach(var eye in eyes)
        {
            eye.BlinkEye();
        }
        triggerCount --;
        if(triggerCount<=0 && !isEnded)
        {
            isEnded = true;
            EventHandler.Call_OnNextGame();
        }
    }
}
