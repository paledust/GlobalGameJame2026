using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTipControl : MonoBehaviour
{
    [SerializeField]
    private float BalanceDegree;
    [SerializeField]
    private float SafeDistance;
    [SerializeField]
    private Transform BodyRoot;
    [SerializeField]
    private Transform RightLegTip;
    [SerializeField]
    private Transform LeftLegTip;
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
        if(Mathf.Abs(BodyRoot.position.x-centerPos.x)>BalanceDegree){
            if(Mathf.Abs(BodyRoot.position.x-rightTipPos.x) >= Mathf.Abs(BodyRoot.position.x-leftTipPos.x)){
                rightTipPos.x = (BodyRoot.position.x>centerPos.x)?BodyRoot.position.x+SafeDistance:BodyRoot.position.x-SafeDistance;
                RightLegTip.position = rightTipPos;
            }
            else{
                leftTipPos.x  = (BodyRoot.position.x>centerPos.x)?BodyRoot.position.x+SafeDistance:BodyRoot.position.x-SafeDistance;
                LeftLegTip.position = leftTipPos;
            }
            centerPos = (rightTipPos+leftTipPos)/2f;
        }
    }
}