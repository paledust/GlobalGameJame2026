using DG.Tweening;
using UnityEngine;
public class Eye : MonoBehaviour
{
    [SerializeField] private float followRadius = 0.2f;
    private Vector3 startPos;
    private Vector3 initScale;
    private bool isClosed;
    void OnEnable()
    {
        startPos = transform.localPosition;
        initScale = transform.localScale;
    }

    public void UpdateEyeTarget(Vector3 targetPos)
    {
        Vector3 localPos = transform.parent.InverseTransformPoint(targetPos);
        localPos.z = startPos.z;
        transform.localPosition = startPos + Vector3.ClampMagnitude(localPos - startPos, followRadius);
    }
    public void CloseEye()
    {
        if(isClosed)
            return;
        isClosed = true;
        transform.DOKill();
        transform.DOScale(new Vector3(initScale.x * 1.5f, initScale.y * 0.2f, initScale.z), 0.1f).SetEase(Ease.OutBack);
    }
    public void OpenEye()
    {
        if(!isClosed)
            return;
        isClosed = false;
        transform.DOKill();
        transform.DOScale(initScale, 0.1f).SetEase(Ease.OutBack);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
