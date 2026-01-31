using UnityEngine;

public class DoubleEyeClick : MonoBehaviour
{
    [SerializeField] private Eye[] eyes;
    private int blinkIndex = 0;
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
        blinkIndex ++;
        blinkIndex = blinkIndex % 3;
        switch(blinkIndex)
        {
            case 0:
                foreach(var eye in eyes)
                {
                    eye.OpenEye();
                }
                break;
            case 1:
                eyes[0].OpenEye();
                eyes[1].CloseEye();
                break;
            case 2:
                eyes[0].CloseEye();
                eyes[1].OpenEye();
                break;
        }
    }
}
