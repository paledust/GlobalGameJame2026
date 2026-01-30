using UnityEngine;

public class MotionControl : MonoBehaviour
{
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Transform bodyRoot;
    [SerializeField]
    private float BalanceDegree;
    [SerializeField]
    private float LerpSpeed;

    Vector3 targetPos;

    void Start()
    {
        targetPos = bodyRoot.transform.position;
        targetPos.x = (headTransform.position.x+bodyRoot.position.x)/2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(bodyRoot.position.x-headTransform.position.x)>=BalanceDegree){
            targetPos.x = (headTransform.position.x+bodyRoot.position.x)/2f;
        }
        bodyRoot.position = Vector3.Lerp(bodyRoot.position, targetPos, Time.deltaTime * LerpSpeed);
    }
}