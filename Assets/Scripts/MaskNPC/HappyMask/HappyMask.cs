using System.Collections;
using UnityEngine;

public class HappyMask : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxHappy;
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
        this.player = player;
    }
    IEnumerator coroutineStartFollow()
    {
        yield return new WaitForSeconds(0.5f);
        isHappy = false;
    }
}
