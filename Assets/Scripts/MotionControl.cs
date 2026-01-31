using UnityEngine;

public class MotionControl : MonoBehaviour
{
    [SerializeField] private Transform followTrans;
    [SerializeField] private float ratio;

    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private float BalanceDegree;
    [SerializeField] private float LerpSpeed;

    Vector3 targetPos;

    void Start()
    {
        targetPos = playerRigid.position;
        targetPos.x = playerRigid.position.x + (followTrans.position.x-playerRigid.position.x)*ratio;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(playerRigid.position.x-followTrans.position.x)>=BalanceDegree){
            targetPos.x = playerRigid.position.x + (followTrans.position.x - playerRigid.position.x) * ratio;
        }
        playerRigid.linearVelocityX = (targetPos.x - playerRigid.position.x) * LerpSpeed;
    }
}