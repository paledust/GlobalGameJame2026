using System.Collections.Generic;
using UnityEngine;

public class ShyMaskController : MonoBehaviour
{
    [SerializeField] private List<ShyMask> shyMasks;
    [SerializeField] private List<ShyMask> shyMasksFront;
    [SerializeField] private Transform heroTrans;
    public void StartShyMaskAppearance()
    {
        foreach(var shyMask in shyMasks)
        {
            shyMask.ReachOut(heroTrans.position - shyMask.transform.position);
        }
    }
}
