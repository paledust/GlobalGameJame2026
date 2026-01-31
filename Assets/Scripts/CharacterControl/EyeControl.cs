using DG.Tweening;
using UnityEngine;

public class EyeControl : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Eye[] eyes;
    [Header("Eye Slot")]
    [SerializeField] private Transform[] eyeSlots;
    private int currentEyeStoneIndex = 0;

    // Update is called once per frame
    void Update()
    {
        foreach(Eye eye in eyes){
            eye.UpdateEyeTarget(targetTrans.position);
        }
    }
    public void InsertEyeStone(GameObject eyeStonePrefab)
    {
        if(eyeStonePrefab==null) return;
        if(currentEyeStoneIndex>=eyeSlots.Length) return;
        Transform eyeSlot = eyeSlots[currentEyeStoneIndex];
        GameObject eyeStone = Instantiate(eyeStonePrefab, eyeSlot);
        Vector3 initScale = eyeStone.transform.localScale;
        eyeStone.transform.localPosition = Vector3.zero;
        eyeStone.transform.localScale = Vector3.zero;
        eyeStone.transform.DOScale(initScale, 0.1f).SetDelay(0.5f).SetEase(Ease.OutBack);
        currentEyeStoneIndex ++;
    }
    public void PopEyeStone()
    {
        currentEyeStoneIndex --;
        Transform eyeSlot = eyeSlots[currentEyeStoneIndex];
        Destroy(eyeSlot.GetChild(0).gameObject);

    }
}
