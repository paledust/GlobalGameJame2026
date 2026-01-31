using UnityEngine;

[CreateAssetMenu(fileName = "item_gem", menuName = "Assets/ItemGemSO")]
public class ItemGemSO : ItemSO
{
    [SerializeField] private GameObject prefabEyeStone;
    public override Item GetItem()
    {
        return new Gem(itemKey, prefabEyeStone);
    }
}
