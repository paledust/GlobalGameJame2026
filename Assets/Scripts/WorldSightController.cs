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
            switch(sightID)
            {
                case "sight_blue":
                    blueBK.SetActive(true);
                    redBK.SetActive(false);
                    break;
                case "sight_red":
                    blueBK.SetActive(false);
                    redBK.SetActive(true);
                    break;
                default:
                    blueBK.SetActive(false);
                    redBK.SetActive(false);
                    break;
            }
            //Change to special sight based on sightID
        }
    }
}
