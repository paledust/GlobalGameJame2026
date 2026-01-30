using UnityEngine;

public class EyeControl : MonoBehaviour
{
    [SerializeField] private float followRadius = 0.2f;
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Eye[] eyes;

    // Update is called once per frame
    void Update()
    {
        foreach(Eye eye in eyes){
            eye.UpdateEyeTarget(targetTrans.position);
        }
    }
}
