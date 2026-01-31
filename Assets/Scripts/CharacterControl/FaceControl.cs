using UnityEngine;

public class FaceControl : MonoBehaviour
{
    [SerializeField] private Transform cursorTrans;
    [SerializeField] private Transform faceTrans;
    [Header("Follow")]
    [SerializeField] private float followFactor = 0.1f;
    [SerializeField] private float followRadius = 2f;
    [SerializeField] private float followLerp = 5f;
    [Header("Rotation")]
    [SerializeField] private float rotateFade = 0.5f;
    [SerializeField] private float rotateLimit = 20f;
    private Vector3 initLocalPos;
    void OnEnable()
    {
        initLocalPos = faceTrans.localPosition;
    }
    void Update()
    {
        Vector3 localPos = faceTrans.parent.InverseTransformPoint(cursorTrans.position);
        Vector3 diff = localPos - initLocalPos;
        Quaternion target = Quaternion.Euler(0, 0, 0);
        float angle = Mathf.Atan(diff.y/Mathf.Abs(diff.x)) * Mathf.Rad2Deg * diff.magnitude/rotateFade;
        angle = Mathf.Clamp(angle, -rotateLimit, rotateLimit);
        if(diff.x>0)
            target = Quaternion.Euler(0, 0, angle);
        else
            target = Quaternion.Euler(0, 0, -angle);
            
        localPos.z = initLocalPos.z;
        Vector3 targetPos = initLocalPos + Vector3.ClampMagnitude(localPos - initLocalPos, followRadius) * followFactor;
        faceTrans.localPosition = Vector3.Lerp(faceTrans.localPosition, targetPos, Time.deltaTime * followLerp);
        faceTrans.localRotation = Quaternion.Slerp(faceTrans.localRotation, target, Time.deltaTime * followLerp);
    }
    public Vector3 GetFaceDirection()
    {
        return cursorTrans.position - faceTrans.position;
    }
}
