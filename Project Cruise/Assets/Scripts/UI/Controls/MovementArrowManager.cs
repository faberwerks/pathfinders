using UnityEngine;

/// <summary>
/// Component to receive movement input values from Movement Arrows.
/// </summary>
public class MovementArrowManager : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    private void Awake()
    {
        Blackboard.Instance.movementArrowManager = this;
    }
}
