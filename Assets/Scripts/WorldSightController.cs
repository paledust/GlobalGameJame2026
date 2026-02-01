using UnityEngine;

public class WorldSightController : MonoBehaviour
{
    [SerializeField] private GameObject blueBK;
    [SerializeField] private GameObject redBK;
    void Start()
    {
        EventHandler.E_OnSwitchSight += OnSwitchSight;
    }
    void OnDestroy()
    {
        EventHandler.E_OnSwitchSight -= OnSwitchSight;
    }
    void OnSwitchSight(string sightID)
    {
        if(string.IsNullOrEmpty(sightID))
        {
            //Restore Normal Sight
            blueBK.SetActive(false);
            redBK.SetActive(false);
        }
        else
        {

            //Change to special sight based on sightID
        }
    }
}
