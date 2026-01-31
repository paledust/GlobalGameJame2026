using System.Collections.Generic;
using UnityEngine;

public class ShyMaskController : MonoBehaviour
{
    [SerializeField] private List<ShyMask> shyMasks;
    [SerializeField] private List<ShyMask> shyMasksFront;
    [SerializeField] private FaceControl heroFace;
    [SerializeField] private ItemGem gem;
    public void StartShyMaskAppearance()
    {
        int gemIndex = Random.Range(0, shyMasks.Count);
        for(int i=0; i<shyMasks.Count; i++)
        {
            if(i == gemIndex)
            {
                shyMasks[i].HoldGem(gem, ReturnGem);
            }
            shyMasks[i].Activate(heroFace);
        }
    }
    void ReturnGem()
    {
        shyMasks.Find(x=>x.IsIdle())?.HoldGem(gem, ReturnGem);
    }
}
