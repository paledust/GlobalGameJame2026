using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteraction
{
    public interface IInteractable
    {
        GameObject gameObject { get; }
        bool m_interactable { get; }
        void OnInteract(PlayerController inputControl, Vector3 hitPos);
        void OnRelease(PlayerController inputControl);
        void OnFailInteract(PlayerController inputControl);
        void ControllingUpdate(PlayerController inputControl);
        void OnExitHover();
        void OnHover(PlayerController inputControl);
    }
    public interface IInteractableUI : IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {}
}