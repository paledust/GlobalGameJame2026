using System.Collections;
using SimpleAudioSystem;
using UnityEngine;

public class PlayerEnterSleep : PlayerDetector
{
    [SerializeField] private string sfxYawn;
    [SerializeField] private GameObject hero;
    [SerializeField] private MotionControl heroMotion;
    [SerializeField] private Home playerHome;
    private bool isTriggered = false;

    protected override void OnFindPlayer(GameObject player)
    {
        if (isTriggered) return;

        isTriggered = true;
        AudioManager.Instance.PlaySoundEffect(sfxYawn, 1);
        StartCoroutine(coroutinePLayerSleep());
    }
    IEnumerator coroutinePLayerSleep()
    {
        yield return new WaitForLoop(1.5f, (t) =>
        {
            heroMotion.ChangeSpeedLimit(Mathf.Lerp(0.5f, 0, t));
        });
        hero.SetActive(false);
        playerHome.StartPlayerSleep();
        yield return new WaitForSeconds(3f);
        GameManager.Instance.SwitchingScene("InBetween");
    }
}
