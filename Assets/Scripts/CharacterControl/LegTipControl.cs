using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LegTipControl : MonoBehaviour
{
    [SerializeField]
    private float BalanceDegree;
    [SerializeField]
    private float SafeDistance;
    [SerializeField]
    private Rigidbody2D playerRigid;
    [SerializeField]
    private Transform RightLegTip;
    [SerializeField]
    private Transform LeftLegTip;
    [Header("Step")]
    [SerializeField] private float stepHeight = 0.2f;
    [SerializeField] private float stepDuration = 0.2f;
    Vector3 rightTipPos;
    Vector3 leftTipPos;
    Vector3 centerPos;
    void Start()
    {
        rightTipPos = RightLegTip.position;
        leftTipPos  = LeftLegTip.position;
        centerPos = (rightTipPos+leftTipPos)/2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(playerRigid.position.x-centerPos.x)>BalanceDegree){
            if(Mathf.Abs(playerRigid.position.x-rightTipPos.x) >= Mathf.Abs(playerRigid.position.x-leftTipPos.x)){
                rightTipPos.x = (playerRigid.position.x>centerPos.x)?playerRigid.position.x+SafeDistance:playerRigid.position.x-SafeDistance;
                MoveTransToPos(RightLegTip, GetRayCastPos(rightTipPos));

            }
            else{
                leftTipPos.x  = (playerRigid.position.x>centerPos.x)?playerRigid.position.x+SafeDistance:playerRigid.position.x-SafeDistance;
                MoveTransToPos(LeftLegTip, GetRayCastPos(leftTipPos));
            }
            centerPos = (rightTipPos+leftTipPos)/2f;
        }
    }
    Vector2 GetRayCastPos(Vector2 refPos)
    {
        return Physics2D.Raycast(refPos + Vector2.up * 1f, Vector2.down, 10f, 1 << Service.PlatformLayer).point;
    }
    void MoveTransToPos(Transform trans,Vector3 targetPos)
    {
        trans.DOKill();
        trans.DOJump(targetPos, stepHeight, 1, stepDuration, false);
    }
}