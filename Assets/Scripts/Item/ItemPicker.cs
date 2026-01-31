using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private enum PickerState
    {
        Idle,
        ReadyToPick,
        Picking,
    }
    [Header("Picker Settings")]
    [SerializeField] private Transform bodyTrans;
    [SerializeField] private float readyHeight = 0.61f;
    [SerializeField] private float pickHeight = 0.4f;
    [SerializeField, ShowOnly] private PickerState pickerState;

    [Header("Container Settings")]
    [SerializeField] private ItemContainer itemContainer;
    private AnimationControl animationControl;
    private HashSet<ItemBasic> pendingItems = new HashSet<ItemBasic>();
    void Start()
    {
        animationControl = GetComponentInParent<AnimationControl>();
        pendingItems = new HashSet<ItemBasic>();
        pickerState = PickerState.Idle;
    }
    void Update()
    {
        switch(pickerState)
        {
            case PickerState.Idle:
                if(bodyTrans.localPosition.y>readyHeight)
                {
                    pickerState = PickerState.ReadyToPick;
                    return;
                }
                break;
            case PickerState.ReadyToPick:
                if(bodyTrans.localPosition.y<=pickHeight)
                {
                    pickerState = PickerState.Picking;
                    return;
                }
                break;
            case PickerState.Picking:
                bool pickFlag = false;
                foreach(var itemBasic in pendingItems.ToArray())
                {
                    if(!pickFlag)
                        pickFlag = true;
                    itemBasic.OnPickUp();
                    itemContainer.StoreItem(itemBasic.GetItem());
                    Destroy(itemBasic.gameObject);
                }
                pendingItems.Clear();

                if(pickFlag)
                {
                    animationControl.TriggerHappy();
                }
                pickerState = PickerState.Idle;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        ItemBasic itemBasic = other.GetComponent<ItemBasic>();
        if(itemBasic != null)
        {
            if(pendingItems==null)
            {
                pendingItems = new HashSet<ItemBasic>();
            }
            pendingItems.Add(itemBasic);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        ItemBasic itemBasic = other.GetComponent<ItemBasic>();
        if(itemBasic != null)
        {
            if(pendingItems==null)
            {
                pendingItems = new HashSet<ItemBasic>();
            }
            pendingItems.Remove(itemBasic);
        }
    }
}
