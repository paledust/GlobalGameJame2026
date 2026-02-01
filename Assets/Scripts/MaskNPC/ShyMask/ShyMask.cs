using System;
using DG.Tweening;
using SimpleAudioSystem;
using UnityEngine;

using Random = UnityEngine.Random;

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
    [SerializeField] private AnimationCurve headRotateCurve;
    [SerializeField] private Vector2 headRotateDelay;
    [SerializeField] private Vector2 headRotateAngle;

    [Header("hide and seek")]
    [SerializeField] private Vector2 reachOutDelay;
    [SerializeField] private Animation shockAnime;

    [Header("Gem")]
    [SerializeField] private Transform gemParent;

    [Header("Audio")]
    [SerializeField] private string sfxReachOut;
    [SerializeField] private string sfxHideBack;
    [SerializeField] private float sfxVolume = 1f;
    
    private float stateTimer = 0;
    private float rotateDelay;
    private float reacahOutDelay;
    private float nextHeadAngle;
    private bool isUnprepared;
    private Vector3 initPos;
    private FaceControl heroFace;
    private ItemGem holdingGem;
    private Action returnGemAction;
    public bool IsIdle()=>currentState == ShyMaskState.Idle;

    void Start()
    {
        initPos = transform.position;
        nextHeadAngle = Random.Range(headRotateAngle.x, headRotateAngle.y);
        rotateDelay = Random.Range(headRotateDelay.x, headRotateDelay.y);
        reacahOutDelay = Random.Range(reachOutDelay.x, reachOutDelay.y);
    }
    public void HoldGem(ItemGem gem, Action returnAction)
    {
        holdingGem = gem;
        this.returnGemAction = returnAction;
        gem.transform.SetParent(gemParent);
        gem.transform.localPosition = Vector3.up*1.6f;
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
                            AudioManager.Instance.PlaySoundEffect(sfxReachOut, 1);
                        }
                    }
                }
                break;
            case ShyMaskState.ReachedOut:
                stateTimer += Time.deltaTime;
                if(stateTimer >= rotateDelay)
                {
                    stateTimer = 0;
                    isUnprepared = true;
                    nextHeadAngle = -Mathf.Sign(nextHeadAngle) * Random.Range(headRotateAngle.x, headRotateAngle.y);
                    headTrans.DOKill();
                    headTrans.DORotate(nextHeadAngle * Vector3.forward, 1f).SetEase(headRotateCurve).OnComplete(()=>isUnprepared = false);
                }

                Vector2 diff = transform.position - heroFace.transform.position;
                if(!CheckGaze())
                {
                    currentState = ShyMaskState.HideBack;
                    if(isUnprepared)
                    {
                        isUnprepared = false;
                        shockAnime.Play();
                        if(holdingGem != null)
                        {
                            var gem = this.holdingGem;
                            gem.transform.SetParent(null);
                            gem.transform.DOMove(transform.position + Vector3.up * 3f, 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
                            {
                                gem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                            });
                            holdingGem = null;
                        }
                        break;
                    }
                    if(holdingGem!=null)
                    {
                        returnGemAction?.Invoke();
                    }
                    returnGemAction = null;
                    holdingGem = null;
                    HideOut();
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
    public void HideOut()
    {
        bodyTrans.DOKill();
        bodyTrans.DORotateQuaternion(Quaternion.Euler(0,0,0), 0.25f).SetEase(Ease.OutQuad);
        transform.DOKill();
        transform.DOMove(initPos, .25f).SetEase(Ease.OutQuad).OnComplete(()=>{
            stateTimer = 0;
            reacahOutDelay = Random.Range(reachOutDelay.x, reachOutDelay.y);
            currentState = ShyMaskState.Idle;
        });
        AudioManager.Instance.PlaySoundEffect(sfxHideBack, sfxVolume);
    }
    public void AE_HideOut()=>HideOut();
}
