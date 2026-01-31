using DG.Tweening;
using UnityEngine;

public abstract class ItemBasic : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private SpriteRenderer shadowRender;
    private Item item;
    void Start()
    {
        item = itemSO.GetItem();
    }
    public abstract void OnPickUp();
    public void OnSelected()
    {
        shadowRender.DOKill();
        shadowRender.DOFade(1, 0.2f);
    }
    public void OnDeselected()
    {
        shadowRender.DOKill();
        shadowRender.DOFade(0, 0.2f);
    }
    public Item GetItem() => item;
}