using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//负责管理玩家身上的物品，以及管理场景中的可拾取道具是否已经被拿走
public class Inventory : MonoBehaviour
{
    private HashSet<Item> items = new HashSet<Item>();
    private Dictionary<string, int> itemCountDict = new Dictionary<string, int>();

    void Start()
    {
        var items = ItemManager.Instance.GetPlayerItems();
        foreach(var item in items)
        {
            StoreItem(item, true);
        }
    }
    public void StoreItem(Item item, bool storeImmediately = false)
    {
        if (item == null) return;
        if(items.Add(item))
        {
            if(storeImmediately)
                item.OnPickedImmediately(this.gameObject);
            else
                item.OnPicked(this.gameObject);
                
            if(itemCountDict.ContainsKey(item.itemKey))
            {
                itemCountDict[item.itemKey]++;
            }
            else
            {
                itemCountDict[item.itemKey] = 1;
            }
            EventHandler.Call_OnPlayerPickItem(item);
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