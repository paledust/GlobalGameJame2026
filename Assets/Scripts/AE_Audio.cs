using SimpleAudioSystem;
using UnityEngine;

public class AE_Audio : MonoBehaviour
{
    [SerializeField] private float overallVolume = 1f;
    public void PlaySFX(string sfxName)
    {
        AudioManager.Instance.PlaySoundEffect(sfxName, overallVolume);
    }
}
