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
    
    private float stateTimer = 0;
    private float rotateDelay;
    private float reacahOutDelay;
    private float nextHeadAngle;
    private Vector3 initPos;
    private FaceControl heroFace;

    void Start()
    {
        initPos = transform.position;
        nextHeadAngle = Random.Range(headRotateAngle.x, headRotateAngle.y);
        rotateDelay = Random.Range(headRotateDelay.x, headRotateDelay.y);
        reacahOutDelay = Random.Range(reachOutDelay.x, reachOutDelay.y);
    }
    void Update()
    {
        switch(currentState)
        {
            case ShyMaskState.Idle:
                if(heroFace != null)
                {
                    if(CheckGaze())
                    {
                        stateTimer += Time.deltaTime;
                        if(stateTimer > reacahOutDelay)
                        {
                            stateTimer = 0;
                            Vector2 direction = heroFace.transform.position - transform.position;
                            float angle = Mathf.Sign(-direction.x) * bodyRotation;
                            bodyTrans.DOKill();
                            bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,angle), 0.25f).SetEase(Ease.OutBack);
                            transform.DOKill();
                            transform.DOMove(initPos + Mathf.Sign(direction.x) * Random.Range(rootMoveDistanceRange.x, rootMoveDistanceRange.y) * Vector3.right, .25f).SetEase(Ease.OutBack);
                            currentState = ShyMaskState.ReachedOut;
                        }
                    }
                }
                break;
            case ShyMaskState.ReachedOut:
                stateTimer += Time.deltaTime;
                if(stateTimer >= rotateDelay)
                {
                    stateTimer = 0;
                    nextHeadAngle = -Mathf.Sign(nextHeadAngle) * Random.Range(headRotateAngle.x, headRotateAngle.y);
                    headTrans.DOKill();
                    headTrans.DORotate(nextHeadAngle * Vector3.forward, 0.25f).SetEase(Ease.OutQuad);
                }

                Vector2 diff = transform.position - heroFace.transform.position;
                if(!CheckGaze())
                {
                    currentState = ShyMaskState.HideBack;
                    bodyTrans.DOKill();
                    bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,0), 0.25f).SetEase(Ease.OutQuad);
                    transform.DOKill();
                    transform.DOMove(initPos, .25f).SetEase(Ease.OutQuad).OnComplete(()=>{
                        stateTimer = 0;
                        reacahOutDelay = Random.Range(reachOutDelay.x, reachOutDelay.y);
                        currentState = ShyMaskState.Idle;
                    });
                }
                break;
            case ShyMaskState.HideBack:
                break;
        }
    }
    public void Activate(FaceControl heroFace)
    {
        this.heroFace = heroFace;
    }
    bool CheckGaze()
    {
        Vector2 diff = transform.position - heroFace.transform.position;
        if(Vector3.Dot(diff.normalized, heroFace.GetFaceDirection().normalized) < 0.2f)
        {
            return true;
        }
        return false;
    }
}
