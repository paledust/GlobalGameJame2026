using System.Collections;
using UnityEngine;

public class InBetween : MonoBehaviour
{
    [SerializeField] private float waitTime = 4f;
    [SerializeField] private string nextSceneName = "Intro";
    void Start()
    {
        StartCoroutine(coroutineDelayLoading());
    }
    IEnumerator coroutineDelayLoading()
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.SwitchingScene(nextSceneName);
    }
}