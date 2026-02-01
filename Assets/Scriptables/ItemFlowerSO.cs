using UnityEngine;

[CreateAssetMenu(fileName = "ItemFlowerSO", menuName = "Assets/ItemFlowerSO")]
public class ItemFlowerSO : ItemSO
{
    [SerializeField] private GameObject flowerPrefab;
    public override Item GetItem()
    {
        return new Flower(itemKey, flowerPrefab);
    }
}
