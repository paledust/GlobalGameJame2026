using DG.Tweening;
using UnityEngine;

public class StrangerFocus : MonoBehaviour
{
    [SerializeField] private Stranger stranger;
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform faceTrans;
    [SerializeField] private Transform kiteTrans;
    [SerializeField] private Transform heroTrans;
    void Start()
    {
        EventHandler.E_OnBlinkEye += OnCheckPlayer;
    }
    void OnDestroy()
    {
        EventHandler.E_OnBlinkEye -= OnCheckPlayer;
    }
    void OnCheckPlayer()
    {
        Vector3 heroPos = heroTrans.position;
        Vector3 focusPos = (heroPos - faceTrans.position) * 0.1f + faceTrans.position;
        focusPoint.DOMove(focusPos, 0.2f).OnComplete(() =>
        {
            var hat = heroTrans.GetComponent<ItemContainer>();
            if(hat.GetItemCount("item_flower")>=3)
            {
                stranger.SmellFlower();
                Destroy(this);
                return;
            }

            focusPoint.DOMove(kiteTrans.position, 0.25f).SetDelay(Random.Range(0.2f,0.5f));
        });
    }
}
