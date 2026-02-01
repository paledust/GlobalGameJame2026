using System;
using SimpleAudioSystem;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] protected string amb_start;
    [SerializeField] protected float amb_volume = 0.1f;
    protected virtual void Start()
    {
        AudioManager.Instance.PlayAmbience(amb_start, true, 0.1f, amb_volume);
    }
}
