using DG.Tweening;
using UnityEngine;

public class StrangerFocus : MonoBehaviour
{
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform faceTrans;
    [SerializeField] private Transform kiteTrans;
    [SerializeField] private Transform heroTrans;
    void Start()
    {
        EventHandler.E_OnBlinkEye += OnSwitchSight;
    }
    void OnDestroy()
    {
        EventHandler.E_OnBlinkEye -= OnSwitchSight;
    }
    void OnSwitchSight()
    {
        Vector3 heroPos = heroTrans.position;
        Vector3 focusPos = (heroPos - faceTrans.position) * 0.1f + faceTrans.position;
        focusPoint.DOMove(focusPos, 0.2f).OnComplete(() =>
        {
            focusPoint.DOMove(kiteTrans.position, 0.25f).SetDelay(Random.Range(0.2f,0.5f));
        });
    }
}
