using PlayerInteraction;
using SimpleAudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystemActions.PlayerActions playerActions;
    public Interactable m_hoveringInteractable{get; private set;} //The hovering interactable.
    public Interactable m_holdingInteractable{get; private set;} //Currently holding interactable.
    public Vector2 PointerScrPos{get; private set;}
    public Vector2 PointerDelta{get; private set;}

    private Vector3 hoverPos;
    private Camera mainCam;
    private float exitCounter;
    public enum SleepingState
    {
        Awake,
        Asleep,
        Sleeping
    }
    private SleepingState currentSleepState = SleepingState.Awake;

    void Start()
    {
        PlayerManager.Instance.RegisterInput(this);
        mainCam = Camera.main;
    }
    void OnEnable()
    {
        playerActions = new InputSystemActions().Player;
        playerActions.Fire.performed += OnFire;
        playerActions.Fire.canceled += OnFireCancel;
        playerActions.PointerPos.performed += OnPointerPos;
        playerActions.PointerMove.performed += OnPointerMove;
        playerActions.Enable();
    }
    void OnDisable()
    {
        playerActions.Fire.performed -= OnFire;
        playerActions.Fire.canceled -= OnFireCancel;
        playerActions.PointerPos.performed -= OnPointerPos;
        playerActions.PointerMove.performed -= OnPointerMove;
        playerActions.Disable();
    }
    void OnDestroy()
    {
        if(PlayerManager.Instance!=null)
            PlayerManager.Instance.UnregisterInput(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_holdingInteractable==null)
        {
            Ray ray = mainCam.ScreenPointToRay(PointerScrPos);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1<<Service.InteractableLayer)){
                Interactable hit_Interactable = hit.collider.GetComponent<Interactable>();
                hoverPos = hit.point;
                if(hit_Interactable!=null){
                    if(m_hoveringInteractable != hit_Interactable) {
                        if(m_hoveringInteractable!=null) m_hoveringInteractable.OnExitHover();

                        m_hoveringInteractable = hit_Interactable;
                        if(m_hoveringInteractable.m_interactable) m_hoveringInteractable.OnHover(this);
                        if(!m_holdingInteractable) PlayerManager.Instance.UpdateCursorState(CURSOR_STATE.HOVER);
                    }
                }
                else{
                    ClearHoveringInteractable();
                }
            }
            else{
                ClearHoveringInteractable();
            }
        }
        else{
            m_holdingInteractable.ControllingUpdate(this);
        }

        switch(currentSleepState)
        {
            case SleepingState.Awake:
                if(!Mouse.current.leftButton.isPressed)
                {
                    exitCounter = 0;
                }
                else
                {
                    exitCounter += Time.deltaTime;
                    if(exitCounter>=2)
                    {
                        EventHandler.Call_OnAsleep();
                        currentSleepState = SleepingState.Asleep;
                    }
                }
                break;
            case SleepingState.Asleep:
                if(!Mouse.current.leftButton.isPressed)
                {
                    exitCounter = 0;
                    EventHandler.Call_OnAwake();
                    currentSleepState = SleepingState.Awake;
                }
                else
                {
                    exitCounter += Time.deltaTime;
                    if(exitCounter>=4)
                    {
                        currentSleepState = SleepingState.Sleeping;
                        GameManager.Instance.SwitchingScene("InBetween");
                    }
                }
                break;
            case SleepingState.Sleeping:
                // Do nothing, already sleeping
                break;
        }
    }

    #region Handle Interactable
    void ClearHoveringInteractable(){
        if(m_hoveringInteractable != null){
            m_hoveringInteractable.OnExitHover();
            m_hoveringInteractable = null;
        }
    }
    void ClearHoldingInteractable(){
        if(m_holdingInteractable != null){
            var holding = m_holdingInteractable;
            m_holdingInteractable = null;
            holding.OnRelease(this);
        }
        else PlayerManager.Instance.UpdateCursorState(CURSOR_STATE.HOVER);
    }
    void InteractWithClickable(){
        if(m_hoveringInteractable.m_interactable){
            m_hoveringInteractable.OnInteract(this, hoverPos);
            AudioManager.Instance.PlaySoundEffect(m_hoveringInteractable.sfx_clickSound, 1);
        }
        else{
            m_hoveringInteractable.OnFailInteract(this);
        }
    }
    public Vector3 GetCursorWorldPoint(float depth){
        Vector3 mousePoint = PointerScrPos;
        mousePoint.z = depth;
        return mainCam.ScreenToWorldPoint(mousePoint);
    }
    public void HoldInteractable(Interactable interactable){
        m_holdingInteractable = interactable;
    }
    public void ReleaseCurrentHolding()=>ClearHoldingInteractable();
    public void CheckControllable(){
        if(PlayerManager.Instance.m_canControl){
            playerActions.Enable();
            this.enabled = true;
        }
        else{
            this.enabled = false;
            if(m_hoveringInteractable) ClearHoveringInteractable();
            if(m_holdingInteractable) ClearHoldingInteractable();
            playerActions.Disable();
        }
    }
#endregion

#region Player Input
    void OnPointerMove(InputAction.CallbackContext context){
        PointerDelta = context.ReadValue<Vector2>();
    }
    void OnPointerPos(InputAction.CallbackContext context){
        Vector2 _scrPos = context.ReadValue<Vector2>();
        PointerScrPos = _scrPos;
    }
    void OnFire(InputAction.CallbackContext context){
        EventHandler.Call_OnBlinkEye();
        PlayerManager.Instance.UpdateCursorState(CURSOR_STATE.Click);
        if(m_holdingInteractable != null) return;
        if(m_hoveringInteractable == null) return;
        InteractWithClickable();
    }
    void OnFireCancel(InputAction.CallbackContext context)
    {
        ClearHoldingInteractable();
        PlayerManager.Instance.UpdateCursorState(CURSOR_STATE.DEFAULT);
    }
#endregion
}
