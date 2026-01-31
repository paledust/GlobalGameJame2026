using System;
using SimpleAudioSystem;
using UnityEngine;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    [Serializable]
    public struct LevelProgression
    {
        public GameObject levelAsset;
        public PlayableDirector startDirector;
        public PlayableDirector endDirector;
        public void UnloadLevel()
        {
            levelAsset.SetActive(false);
        }
        public void LoadLevel(bool autoPlay = true)
        {
            if(startDirector!=null && autoPlay)
                startDirector.Play();
            levelAsset.SetActive(true);
        }
    }

    [SerializeField] private string amb_start;
    [SerializeField] private int startIndex;
    [SerializeField] private LevelProgression[] levelProgressions;
    private int progressionIndex = 0;

    void Start()
    {
        AudioManager.Instance.PlayAmbience(amb_start, true, 0.5f, 1);
        foreach(var level in levelProgressions)
        {
            level.UnloadLevel();
        }
        progressionIndex = startIndex; 
        levelProgressions[progressionIndex].LoadLevel();
        EventHandler.E_OnNextGame += OnNextGame;
    }
    void OnDestroy()
    {
        EventHandler.E_OnNextGame -= OnNextGame;
    }
    void OnNextGame()
    {
        if(progressionIndex>=levelProgressions.Length) 
            return;
        levelProgressions[progressionIndex].endDirector.Play();
        progressionIndex ++;
    }
    [ContextMenu("Debug_Start_At_Index_Level")]
    public void StartAtIndexLevel()
    {
        progressionIndex = startIndex;
        levelProgressions[progressionIndex].LoadLevel(false);
    }
}