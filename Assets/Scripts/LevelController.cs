using SimpleAudioSystem;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string amb_start;
    [Header("2nd Intro")]
    [SerializeField] private PlayableDirector singleEyeToDoubleEyeDirector;
    void Start()
    {
        AudioManager.Instance.PlayAmbience(amb_start, true, 0.5f, 1);        
        EventHandler.E_OnSingleEyeIntroEnd += OnSingleEyeIntroEnd;
    }
    void OnDestroy()
    {
        EventHandler.E_OnSingleEyeIntroEnd -= OnSingleEyeIntroEnd;
    }
    void OnSingleEyeIntroEnd()
    {
        singleEyeToDoubleEyeDirector.Play();
    }
}