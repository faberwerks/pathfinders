using UnityEngine;

/// <summary>
/// Component to receive movement input values from Movement Arrows.
/// </summary>
public class MovementArrowManager : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    private void Awake()
    {
        Blackboard.Instance.MovementArrowManager = this;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) { Direction = new Vector2(0,1); }
        else if (Input.GetKey(KeyCode.A)) { Direction = new Vector2(-1,0); }
        else if (Input.GetKey(KeyCode.S)) { Direction = new Vector2(0,-1); }
        else if (Input.GetKey(KeyCode.D)) { Direction = new Vector2(1,0); }
        else if (Input.GetKey(KeyCode.Q)) { Direction = new Vector2(-1,1); }
        else if (Input.GetKey(KeyCode.E)) { Direction = new Vector2(1,1); }
        else if (Input.GetKey(KeyCode.Z)) { Direction = new Vector2(-1,-1); }
        else if (Input.GetKey(KeyCode.C)) { Direction = new Vector2(1,-1); }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C)) Direction = Vector2.zero;
    }
#endif
}
