using SimpleAudioSystem;
using UnityEngine;

public class PlayerEnterYawn : PlayerDetector
{
    [SerializeField] private string sfxYawn;
    protected override void OnFindPlayer(GameObject player)
    {
        AudioManager.Instance.PlaySoundEffect(sfxYawn, 1);
    }
}
