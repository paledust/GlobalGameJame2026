using UnityEngine;

public class GameProgressionManager : Singleton<GameProgressionManager>
{
    public bool isIntroOver{get; private set;} = false;
    public bool isBlueGemPicked{get; private set;} = false;
    public bool isRedGemPicked{get; private set;} = false;
    private const string BLUE_GEM_KEY = "item_gem_blue";
    private const string RED_GEM_KEY = "item_gem_red";

    protected override void Awake()
    {
        base.Awake();
        isIntroOver = false;
        isBlueGemPicked = false;
        isRedGemPicked = false;
        EventHandler.E_OnPlayerPickItem += OnPlayerPickItem;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventHandler.E_OnPlayerPickItem -= OnPlayerPickItem;
    }
    private void OnPlayerPickItem(Item item)
    {
        if (item == null) return;
        if(item.itemKey == BLUE_GEM_KEY)
        {
            isBlueGemPicked = true;
        }
        else if(item.itemKey == RED_GEM_KEY)
        {
            isRedGemPicked = true;
        }
    }
    public void EndIntro()
    {
        isIntroOver = true;
    }
    public void ResetProgress()
    {
        isIntroOver = false;
        isBlueGemPicked = false;
        isRedGemPicked = false;
    }
}