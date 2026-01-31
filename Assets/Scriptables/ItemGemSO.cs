using UnityEngine;

[CreateAssetMenu(fileName = "item_gem", menuName = "Assets/ItemGemSO")]
public class ItemGemSO : ItemSO
{
    [SerializeField] private string sightKey;
    [SerializeField] private GameObject prefabEyeStone;
    public override Item GetItem()
    {
        return new Gem(itemKey, sightKey, prefabEyeStone);
    }
}
