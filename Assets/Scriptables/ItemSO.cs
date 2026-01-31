using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [SerializeField] protected string itemKey;
    public abstract Item GetItem();
}
