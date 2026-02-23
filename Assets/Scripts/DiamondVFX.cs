using UnityEngine;

public class DiamondVFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer redRenderer;
    [SerializeField] private SpriteRenderer blueRenderer;
    private const string BLUE_GEM_KEY = "item_gem_blue";
    private const string RED_GEM_KEY = "item_gem_red";
    void Awake()
    {
        EventHandler.E_OnPlayerPickItem += OnPlayerPickItem;
    }
    void OnDestroy()
    {
        EventHandler.E_OnPlayerPickItem -= OnPlayerPickItem;
    }
    void OnPlayerPickItem(Item item)
    {
        if(item.itemKey == BLUE_GEM_KEY)
        {
            blueRenderer.enabled = true;
        }
        else if(item.itemKey == RED_GEM_KEY)
        {
            redRenderer.enabled = true;
        }
    }
}
