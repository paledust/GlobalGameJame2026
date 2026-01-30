using UnityEngine;

public class BodyControl : MonoBehaviour
{
    [SerializeField] private Transform faceTrans;
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float rotateFactor;
    // Update is called once per frame
    void Update()
    {
        float offset = faceTrans.localPosition.x;
        bodyTrans.localRotation = Quaternion.Euler(0, 0, -offset * rotateFactor);
    }
}
