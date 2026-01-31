using SimpleAudioSystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string amb_start;
    void Start()
    {
        AudioManager.Instance.PlayAmbience(amb_start, true, 0.5f, 1);        
    }
}
