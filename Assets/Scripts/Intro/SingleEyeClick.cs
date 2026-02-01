using SimpleAudioSystem;
using UnityEngine;

public class SingleEyeClick : MonoBehaviour
{
    [SerializeField] private Animation eyeMask;
    [SerializeField] private int count = 3;
    [SerializeField] private string blinkSFX;
    [SerializeField] private string finalSFX;
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
        count --;
        AudioManager.Instance.PlaySoundEffect(blinkSFX, 1);
        if(count<=0)
        {
            eyeMask.Play("intro_open");
            AudioManager.Instance.PlaySoundEffect(finalSFX, 1);
            EventHandler.Call_OnNextGame();
            Destroy(this.gameObject);
        }
        else
        {
            eyeMask.Play();
        }
    }
}
