using UnityEngine;

public class HappyMask : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxHappy;
    private Rigidbody2D rigid2D;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    public void OnFindPlayer()
    {
        rigid2D.AddForce(Vector2.up * 5f,ForceMode2D.Impulse);
        vfxHappy.Play();
    }
}
