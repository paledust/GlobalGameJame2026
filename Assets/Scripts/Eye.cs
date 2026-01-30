using UnityEngine;
public class Eye : MonoBehaviour
{
    [SerializeField] private float followRadius = 0.2f;
    private Vector3 startPos;
    void OnEnable()
    {
        startPos = transform.localPosition;
    }

    public void UpdateEyeTarget(Vector3 targetPos)
    {
        Vector3 localPos = transform.parent.InverseTransformPoint(targetPos);
        localPos.z = startPos.z;
        transform.localPosition = startPos + Vector3.ClampMagnitude(localPos - startPos, followRadius);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
