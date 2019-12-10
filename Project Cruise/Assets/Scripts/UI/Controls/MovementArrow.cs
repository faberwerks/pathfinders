using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Component to handle movement input.
/// </summary>
public class MovementArrow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MovementArrowManager manager = null;
    public float xInput = 0.0f;
    public float yInput = 0.0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        manager.Direction = new Vector2(xInput, yInput);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        manager.Direction = new Vector2(0.0f, 0.0f);
    }
}
