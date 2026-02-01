using DG.Tweening;
using UnityEngine;

public class Stranger : MonoBehaviour
{
    [SerializeField] private AnimationControl animationControl;
    [SerializeField] private Transform kiteTrans;
    [SerializeField] private Transform pullCloseTrans;
    [SerializeField] private Rigidbody2D gemRigid;
    [SerializeField] private Transform focusPoint;
    [SerializeField] private Transform focusKite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SmellFlower()
    {
        animationControl.TriggerHappy();
        focusPoint.DOMove(focusKite.position, 1f).SetEase(Ease.InOutQuad);
        kiteTrans.DOMove(pullCloseTrans.position, 4f).SetDelay(2f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            gemRigid.transform.parent = null;
            gemRigid.bodyType = RigidbodyType2D.Dynamic;
            gemRigid.AddForce(Vector3.up*2, ForceMode2D.Impulse);
        });
    }
}
