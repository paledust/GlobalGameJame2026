using DG.Tweening;
using UnityEngine;

public class ShyMask : MonoBehaviour
{
    public enum ShyMaskState
    {
        Idle,
        ReachedOut,
        HideBack
    }
    [SerializeField] private ShyMaskState currentState = ShyMaskState.Idle;
    [Header("Body")]
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float bodyRotation;
    [SerializeField] private Vector2 rootMoveDistanceRange;

    [Header("Head")]
    [SerializeField] private Transform headTrans;
    [SerializeField] private Vector2 headRotateDelay;
    [SerializeField] private Vector2 headRotateAngle;

    [Header("hide and seek")]
    [SerializeField] private Vector2 reachOutDelay;
    
    private float headTimer = 0;
    private float headDelay;
    private float nextHeadAngle;
    private Vector3 initPos;
    private Transform playerTrans;

    void Start()
    {
        initPos = transform.position;
        nextHeadAngle = Random.Range(headRotateAngle.x, headRotateAngle.y);
        headDelay = Random.Range(headRotateDelay.x, headRotateDelay.y);
    }
    void Update()
    {
        switch(currentState)
        {
            case ShyMaskState.Idle:
                break;
            case ShyMaskState.ReachedOut:
                headTimer += Time.deltaTime;
                if(headTimer > headDelay)
                {
                    headTimer = 0;
                    nextHeadAngle = -Mathf.Sign(nextHeadAngle) * Random.Range(headRotateAngle.x, headRotateAngle.y);
                    headTrans.DOKill();
                    headTrans.DORotate(nextHeadAngle * Vector3.forward, 0.25f).SetEase(Ease.OutQuad);
                }
                break;
            case ShyMaskState.HideBack:
                break;
        }
    }
    public void Activate(Transform playerTrans)
    {
        this.playerTrans = playerTrans;
    }
    void ReachOut(Vector3 direction)
    {
        currentState = ShyMaskState.ReachedOut;
        float angle = Mathf.Sign(-direction.x) * bodyRotation;
        bodyTrans.DOKill();
        bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,angle), 0.25f).SetEase(Ease.OutBack);
        transform.DOKill();
        transform.DOMove(initPos + Mathf.Sign(direction.x) * Random.Range(rootMoveDistanceRange.x, rootMoveDistanceRange.y) * Vector3.right, .25f).SetEase(Ease.OutBack);
    }
    void HideBack()
    {
        currentState = ShyMaskState.HideBack;
        bodyTrans.DOKill();
        bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,0), 0.25f).SetEase(Ease.OutQuad);
        transform.DOKill();
        transform.DOMove(initPos, .25f).SetEase(Ease.OutQuad).OnComplete(()=>{
            currentState = ShyMaskState.Idle;
        });
    }
}
