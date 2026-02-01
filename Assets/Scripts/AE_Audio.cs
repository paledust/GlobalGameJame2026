using SimpleAudioSystem;
using UnityEngine;

public class AE_Audio : MonoBehaviour
{
    [SerializeField] private float overallVolume = 1f;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private bool stopLoopOnOnceSFX = false;
    public void PlaySFX(string sfxName)
    {
        AudioManager.Instance.PlaySoundEffect(sfxName, overallVolume);
        if(stopLoopOnOnceSFX)
        {
            loopSource.Stop();
        }
    }
    public void PlayLoop(string sfxName)
    {
        if(!loopSource.isPlaying || loopSource.clip==null || loopSource.clip.name!=sfxName)
        {
            AudioManager.Instance.PlaySoundEffectLoop(loopSource, sfxName, overallVolume);
        }
    }
}
