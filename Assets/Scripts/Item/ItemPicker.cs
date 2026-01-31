using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField] private float readyDuration = 0.5f;
    [SerializeField, ShowOnly] private PickerState pickerState;

    [Header("Picker Face Settings")]
    [SerializeField] private SortingGroup faceSortingGroup;

    [Header("Container Settings")]
    [SerializeField] private ItemContainer itemContainer;
    private AnimationControl animationControl;
    private HashSet<ItemBasic> pendingItems = new HashSet<ItemBasic>();
    private float pickTimer = 0;

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
                    pickTimer = 0;
                    faceSortingGroup.enabled = true;
                    foreach(var itemBasic in pendingItems)
                    {
                        itemBasic.OnSelected();
                    }
                    return;
                }
                break;
            case PickerState.ReadyToPick:
                if(bodyTrans.localPosition.y<=pickHeight)
                {
                    pickerState = PickerState.Picking;
                    return;
                }
                if(bodyTrans.localPosition.y<=readyHeight)
                {
                    pickTimer += Time.deltaTime;
                    if(pickTimer>=readyDuration)
                    {
                        pickerState = PickerState.Idle;
                        foreach(var itemBasic in pendingItems)
                        {
                            itemBasic.OnDeselected();
                        }
                        faceSortingGroup.enabled = false;
                        return;
                    }
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
                faceSortingGroup.enabled = false;
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
            if(pickerState == PickerState.ReadyToPick)
            {
                itemBasic.OnSelected();
            }
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
            itemBasic.OnDeselected();
        }
    }
}
