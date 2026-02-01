using System.Collections;
using SimpleAudioSystem;
using UnityEngine;

public class HappyMask : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxHappy;
    [SerializeField] private string sfxHappy;
    [SerializeField] private string sfxOnTouch;
    [SerializeField] private float sfxVolume = 0.5f;
    private GameObject player;
    private Rigidbody2D rigid2D;
    private bool isHappy = false;
    private CoroutineExcuter happyCoroutine;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        happyCoroutine = new CoroutineExcuter(this);
    }
    void Update()
    {
        if(player!=null && !isHappy)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rigid2D.AddTorque(-Mathf.Sign(dir.x) * 0.2f);
        }
    }
    public void OnFindPlayer(GameObject player)
    {
        isHappy = true;
        rigid2D.AddForce(Vector2.up * 5f,ForceMode2D.Impulse);
        vfxHappy.Play();
        happyCoroutine.Excute(coroutineStartFollow());
        if(player != null)
        {
            AudioManager.Instance.PlaySoundEffect(sfxHappy, sfxVolume);
            AudioManager.Instance.PlaySoundEffect(sfxOnTouch, sfxVolume);
        }
        this.player = player;
    }
    IEnumerator coroutineStartFollow()
    {
        yield return new WaitForSeconds(0.5f);
        isHappy = false;
    }
}
