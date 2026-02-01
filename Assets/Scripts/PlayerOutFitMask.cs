using SimpleAudioSystem;
using UnityEngine;

public class PlayerOutFitMask : PlayerDetector
{
    [SerializeField] private string sfxChange;
    protected override void OnFindPlayer(GameObject player)
    {
        player.GetComponent<PlayerMaskController>().ChangeMask();
        AudioManager.Instance.PlaySoundEffect(sfxChange, 1);
    }
}
