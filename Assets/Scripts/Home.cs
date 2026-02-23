using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private Animation sleepAnim;
    public void StartPlayerSleep()
    {
        sleepAnim.Play();
    }
}