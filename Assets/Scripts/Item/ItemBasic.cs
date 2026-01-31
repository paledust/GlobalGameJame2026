using UnityEngine;

public abstract class ItemBasic : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;
    private Item item;
    void Start()
    {
        item = itemSO.GetItem();
    }
    public abstract void OnPickUp();
    public Item GetItem() => item;
}