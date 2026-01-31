using DG.Tweening;
using UnityEngine;

public class EyeControl : MonoBehaviour
{
    [SerializeField] private Transform targetTrans;
    [SerializeField] private Eye[] eyes;
    [Header("Eye Slot")]
    [SerializeField] private Transform[] eyeSlots;
    private string[] eyeStoneIDs = new string[2]{string.Empty, string.Empty};
    private int currentEyeStoneIndex = 0;

    // Update is called once per frame
    void Update()
    {
        foreach(Eye eye in eyes){
            eye.UpdateEyeTarget(targetTrans.position);
        }
    }
    public void InsertEyeStone(GameObject eyeStonePrefab, string stoneID)
    {
        if(eyeStonePrefab==null) return;
        if(currentEyeStoneIndex>=eyeSlots.Length) return;
        Transform eyeSlot = eyeSlots[currentEyeStoneIndex];
        GameObject eyeStone = Instantiate(eyeStonePrefab, eyeSlot);
        eyeStoneIDs[currentEyeStoneIndex] = stoneID;
        Vector3 initScale = eyeStone.transform.localScale;
        eyeStone.transform.localPosition = Vector3.zero;
        eyeStone.transform.localScale = Vector3.zero;
        eyeStone.transform.DOScale(initScale, 0.1f).SetDelay(0.5f).SetEase(Ease.OutBack);
        currentEyeStoneIndex ++;
    }
    public void CheckEye(int index)
    {
        switch(index)
        {
            case 0:
                EventHandler.Call_OnBothEye();
                foreach(var eye in eyes)
                {
                    eye.OpenEye();
                }
                break;
            case 1:
                eyes[0].OpenEye();
                eyes[1].CloseEye();
                EventHandler.Call_OnSwitchSight(eyeStoneIDs[index-1]);
                break;
            case 2:
                eyes[0].CloseEye();
                eyes[1].OpenEye();
                EventHandler.Call_OnSwitchSight(eyeStoneIDs[index-1]);
                break;

        }
    }
    public void PopEyeStone()
    {
        currentEyeStoneIndex --;
        Transform eyeSlot = eyeSlots[currentEyeStoneIndex];
        Destroy(eyeSlot.GetChild(0).gameObject);
        eyeStoneIDs[currentEyeStoneIndex] = string.Empty;
    }
}
