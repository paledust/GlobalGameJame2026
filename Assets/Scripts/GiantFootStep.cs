using Unity.Cinemachine;
using UnityEngine;

public class GiantFootStep : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] private LegTipControl legTipControl;
    [SerializeField] private Transform giantTrans;
    [SerializeField] private float impulseFadeDistance = 10;
    void Start()
    {
        legTipControl.OnStepDown += HandleOnStepDown;
    }
    void OnDestroy()
    {
        legTipControl.OnStepDown -= HandleOnStepDown;
    }
    void HandleOnStepDown()
    {
        float force = 1-Mathf.Abs(giantTrans.position.x/impulseFadeDistance);
        force = Mathf.Clamp01(force);
        cinemachineImpulseSource.GenerateImpulse(force);
    }
}
