using UnityEngine;

public class SightController : MonoBehaviour
{
    [SerializeField] private EyeControl eyeControl;
    [SerializeField] private int MaxSight = 3;
    private int sightIndex = 0;
    void OnEnable()
    {
        EventHandler.E_OnBlinkEye += OnSwitchSight;
    }
    void OnDisable()
    {
        EventHandler.E_OnBlinkEye -= OnSwitchSight;
    }
    void OnSwitchSight()
    {
        sightIndex ++;
        if(sightIndex>=MaxSight) sightIndex = 0;
        eyeControl.CheckEye(sightIndex);
    }
}