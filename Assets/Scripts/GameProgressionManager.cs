using UnityEngine;

public class GameProgressionManager : Singleton<GameProgressionManager>
{
    [SerializeField] private bool forceRevealed = false;
    public bool isIntroOver{get; private set;} = false;
    public bool isBlueGemPicked{get; private set;} = false;
    public bool isRedGemPicked{get; private set;} = false;
    private const string BLUE_GEM_KEY = "item_gem_blue";
    private const string RED_GEM_KEY = "item_gem_red";
    private const string GLOBAL_REVEAL_KEY = "IS_REVEALED";

    protected override void Awake()
    {
        base.Awake();
        isIntroOver = false;
        isBlueGemPicked = false;
        isRedGemPicked = false;
        EventHandler.E_OnPlayerPickItem += OnPlayerPickItem;
    }
    void Update()
    {
        if(forceRevealed)
        {
            Shader.SetGlobalInt(GLOBAL_REVEAL_KEY, 1);
        }
        else
        {
            Shader.SetGlobalInt(GLOBAL_REVEAL_KEY, (isBlueGemPicked && isRedGemPicked) ? 1 : 0);
        }
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

        if(isBlueGemPicked && isRedGemPicked)
        {
            Shader.SetGlobalInt(GLOBAL_REVEAL_KEY, 1);
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