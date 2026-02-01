using System.Collections;
using SimpleAudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Animation uiAnimation;
    [SerializeField] private AudioSource m_audio;
    [SerializeField] private AudioClip startSFX;
[Header("Input")]
    [SerializeField] private InputAction press;
    void Start(){
        StartCoroutine(coroutineTitle());
    }
    void OnEnable(){
        press.performed += StartGame;
    }
    void OnDisable(){
        press.performed -= StartGame;
    }
    IEnumerator coroutineTitle(){
        uiAnimation.Play();
        yield return new WaitForSeconds(uiAnimation.clip.length);
        press.Enable();
    }
    void StartGame(InputAction.CallbackContext context){
        press.Disable();
        m_audio.PlayOneShot(startSFX);
        this.enabled = false;
        StartCoroutine(coroutineStartGame());
    }
    IEnumerator coroutineStartGame(){
        uiAnimation.Play("TitleScreen_Out");
        AudioManager.Instance.PlayAmbience("amb_forest", true, 3, 0.5f);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.SwitchingScene("Level_00");
    }
}
