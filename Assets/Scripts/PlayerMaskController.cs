using UnityEngine;

public class PlayerMaskController : MonoBehaviour
{
    [SerializeField] private Sprite[] mask;
    [SerializeField] private SpriteRenderer maskRenderer;

    private int shuffleIndex = 0;

    void Start()
    {
        Service.Shuffle(ref mask);
    }
    public void ChangeMask()
    {
        if(!maskRenderer.enabled)
        {
            maskRenderer.enabled = true;
        }
        maskRenderer.sprite = mask[shuffleIndex % mask.Length];
        shuffleIndex++;
        if(shuffleIndex >= mask.Length)
        {
            Service.Shuffle(ref mask);
            shuffleIndex = 0;
        }
    }
}
