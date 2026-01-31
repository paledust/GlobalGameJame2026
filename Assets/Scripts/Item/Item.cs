using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public string itemKey{get; protected set;}
    public Item(string key){this.itemKey = key;}
    protected GameObject picker;
    public virtual void OnPicked(GameObject picker){this.picker = picker;}
    public virtual void OnDropped(GameObject picker){this.picker = null;}
}

public class Gem : Item
{
    public GameObject prefabEyeStone{get; private set;}
    private EyeControl eyeControl;
    private string sightKey;
    public Gem(string key, string sightKey, GameObject prefabEyeStone) : base(key)
    {
        this.sightKey = sightKey;
        this.prefabEyeStone = prefabEyeStone;
    }
    public override void OnPicked(GameObject picker)
    {
        base.OnPicked(picker);
        eyeControl = picker.GetComponent<EyeControl>();
        eyeControl.InsertEyeStone(prefabEyeStone, sightKey);
    }
    public override void OnDropped(GameObject picker)
    {
        base.OnDropped(picker);
        eyeControl.PopEyeStone();
        eyeControl = null;
    }
}