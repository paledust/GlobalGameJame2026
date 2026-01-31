using UnityEngine;

public class SingleEyeClick : MonoBehaviour
{
    [SerializeField] private Animation eyeMask;
    [SerializeField] private int count = 3;
    void Start()
    {
        EventHandler.E_OnSwitchEye += SwichEyeMask;
    }
    void OnDestroy()
    {
        EventHandler.E_OnSwitchEye -= SwichEyeMask;
    }
    void SwichEyeMask()
    {
        count --;
        if(count<=0)
        {
            eyeMask.Play("intro_open");
            EventHandler.Call_OnNextGame();
            Destroy(this.gameObject);
        }
        else
        {
            eyeMask.Play();
        }
    }
}
