using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private bool IsInTransition;
    private PlayerController currentPlayer;
    
    public bool m_canControl => !IsInTransition;
    public bool m_isHovering => currentPlayer.m_hoveringInteractable!=null;

    void HideCursor()=>Cursor.visible = false;
    void ShowCursor()=>Cursor.visible = true;

    void Update(){
        if(currentPlayer!=null){
            UI_Manager.Instance.UpdateCursorPos(currentPlayer.PointerScrPos);
        }
    }
    void TransitionBeginHandler(){
        IsInTransition = true;
        currentPlayer?.CheckControllable();
    }
    void TransitionEndHandler(){
        IsInTransition = false;
        currentPlayer?.CheckControllable();
    }
    void FlashInputHandler(){
        currentPlayer?.ReleaseCurrentHolding();
    }
    internal void RegisterInput(PlayerController input)
    {
        currentPlayer = input;
    }
    internal void UnregisterInput(PlayerController input)
    {
        if(currentPlayer == input)
            currentPlayer = null;
    }
    public Vector3 GetCursorWorldPos(float depth)=>currentPlayer.GetCursorWorldPoint(depth);
    public void UpdateCursorState(CURSOR_STATE newState)=>UI_Manager.Instance.UpdateCursorState(newState);
}