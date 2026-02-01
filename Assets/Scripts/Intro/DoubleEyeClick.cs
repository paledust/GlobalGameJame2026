using SimpleAudioSystem;
using UnityEngine;

public class DoubleEyeClick : MonoBehaviour
{
    [SerializeField] private Eye[] eyes;
    [SerializeField] private int triggerCount = 4;
    [SerializeField] private string blinkSFX;
    private int blinkIndex = 0;
    private bool isEnded = false;
    void OnEnable()
    {
        EventHandler.E_OnBlinkEye += SwichEyeMask;
    }
    void OnDisable()
    {
        EventHandler.E_OnBlinkEye -= SwichEyeMask;   
    }
    void SwichEyeMask()
    {
        blinkIndex ++;
        blinkIndex = blinkIndex % 3;
        AudioManager.Instance.PlaySoundEffect(blinkSFX, 1);
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
