using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField] private Transform faceTrans;
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float rotateFactor;
    [SerializeField] private float moveFactor;
    [SerializeField] private float startHeight;
    private Vector3 faceInitialPos;
    void Start()
    {
        faceInitialPos = faceTrans.localPosition;
    }
    void Update()
    {
        Vector2 offset = faceTrans.localPosition - faceInitialPos;
        bodyTrans.localRotation = Quaternion.Euler(0, 0, -offset.x * rotateFactor);
        bodyTrans.localPosition = new Vector3(bodyTrans.localPosition.x, startHeight + Mathf.Min(0, offset.y) * moveFactor, bodyTrans.localPosition.z);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 pos = bodyTrans.localPosition;
        pos.y = startHeight;
        Gizmos.matrix = bodyTrans.parent.localToWorldMatrix;
        Gizmos.DrawSphere(pos, 0.02f);
    }
}
