using System.Collections.Generic;
using UnityEngine;

//负责管理玩家身上的物品，以及管理场景中的可拾取道具是否已经被拿走
public class ItemManager : Singleton<ItemManager>
{
    private HashSet<Item> playerItems = new HashSet<Item>();
    private Dictionary<int, bool> itemPickStatus = new Dictionary<int, bool>();
    protected override void Awake()
    {
        base.Awake();
        playerItems = new HashSet<Item>();
        itemPickStatus = new Dictionary<int, bool>();
        EventHandler.E_OnPlayerPickItem += OnPlayerPickItem;
        EventHandler.E_AfterLoadScene += OnAfterLoadScene;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventHandler.E_OnPlayerPickItem -= OnPlayerPickItem;
        EventHandler.E_AfterLoadScene -= OnAfterLoadScene;
    }

    private void OnPlayerPickItem(Item item)
    {
        if (item == null) return;
        if(playerItems.Add(item))
        {
            itemPickStatus[item.uid] = true;
        }
    }
    private void OnAfterLoadScene()
    {
        var allItems = FindObjectsByType<ItemBasic>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        foreach(var itemBasic in allItems)
        {
            int uid = itemBasic.GetItemUID();
            if(itemPickStatus.TryGetValue(uid, out bool isPicked) && isPicked)
            {
                itemBasic.gameObject.SetActive(false);
            }
        }
    }
    public void ClearItemStatus()
    {
        playerItems.Clear();
        itemPickStatus.Clear();
    }
    public HashSet<Item> GetPlayerItems()
    {
        return playerItems;
    }
    public bool IsItemPicked(int uid)
    {
        if(itemPickStatus.TryGetValue(uid, out bool isPicked))
        {
            return isPicked;
        }
        return false;
    }
}
