using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private HashSet<Item> items = new HashSet<Item>();
    public void StoreItem(Item item)
    {
        if (item == null) return;
        items.Add(item);
    }
    public void PopItem(Item item)
    {
        if (item == null) return;
        items.Remove(item);
    }
    public bool HasItem(string key, out Item item)
    {
        if(items!=null)
        {
            item = items.FirstOrDefault(i => i.itemKey == key);
            return item != null;
        }
        item = null;
        return false;
    }
}
