using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private ParticleSystem clickVFX;
    void Start()
    {
        EventHandler.E_OnBlinkEye += OnCloseEye;
    }
    void OnDestroy()
    {
        EventHandler.E_OnBlinkEye -= OnCloseEye;
    }
    void OnCloseEye()
    {
        var pos = PlayerManager.Instance.GetCursorWorldPos(10);
        clickVFX.Emit(new ParticleSystem.EmitParams(){position=pos}, 1);
    }
}
