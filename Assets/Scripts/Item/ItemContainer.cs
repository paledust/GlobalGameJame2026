using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    private HashSet<Item> items = new HashSet<Item>();
    private Dictionary<string, int> itemCountDict = new Dictionary<string, int>();
    public void StoreItem(Item item)
    {
        if (item == null) return;
        if(items.Add(item))
        {
            item.OnPicked(this.gameObject);
            if(itemCountDict.ContainsKey(item.itemKey))
            {
                itemCountDict[item.itemKey]++;
            }
            else
            {
                itemCountDict[item.itemKey] = 1;
            }
        }
    }
    public void PopItem(Item item)
    {
        if (item == null) return;
        if(items.Remove(item))
        {
            item.OnDropped(this.gameObject);
            if(itemCountDict.ContainsKey(item.itemKey))
            {
                itemCountDict[item.itemKey]--;
                if(itemCountDict[item.itemKey] <= 0)
                {
                    itemCountDict.Remove(item.itemKey);
                }
            }
        }
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
    public int GetItemCount(string key)
    {
        if(itemCountDict.TryGetValue(key, out int count))
        {
            return count;
        }
        return 0;
    }
}
