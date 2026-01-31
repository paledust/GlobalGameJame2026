using DG.Tweening;
using UnityEngine;

public class ShyMask : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float bodyRotation;
    [SerializeField] private Vector2 rootMoveDistanceRange;
    [Header("Head")]
    [SerializeField] private Transform headTrans;
    [SerializeField] private Vector2 headRotateDelay;
    [SerializeField] private Vector2 headRotateAngle;
    private bool isReachedOut = false;
    private float headTimer = 0;
    private float headDelay;
    private float nextHeadAngle;
    private Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
        nextHeadAngle = Random.Range(headRotateAngle.x, headRotateAngle.y);
        headDelay = Random.Range(headRotateDelay.x, headRotateDelay.y);
    }
    void Update()
    {
        if(isReachedOut)
        {
            headTimer += Time.deltaTime;
            if(headTimer>headDelay)
            {
                headTimer = 0;
                nextHeadAngle = -Mathf.Sign(nextHeadAngle) * Random.Range(headRotateAngle.x, headRotateAngle.y);
                headTrans.DOKill();
                headTrans.DORotate(nextHeadAngle * Vector3.forward, 0.5f).SetEase(Ease.OutQuad);
            }
        }     
    }
    public void ReachOut(Vector3 direction)
    {
        isReachedOut = true;
        float angle = Mathf.Sign(-direction.x) * bodyRotation;
        bodyTrans.DOKill();
        bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,angle), 2f);
        transform.DOKill();
        transform.DOMove(initPos + Mathf.Sign(direction.x) * Random.Range(rootMoveDistanceRange.x, rootMoveDistanceRange.y) * Vector3.right, 2f);
    }
    public void HideBack()
    {
        isReachedOut = false;
    }
}
